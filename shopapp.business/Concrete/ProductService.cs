using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.business.Concrete;

//[LogAspect(Priority = 1)]
public class ProductService : GenericService<Product, ProductDTO>, IProductService
{
    private readonly IProductRepository _genericRepository;
    public ProductService(IProductRepository genericRepository) : base(genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Response<NoDataDTO>> ReductionStock(int productId, int quantity)
    {
        var product = await _genericRepository.GetByIdAsync(productId);
        if (product == null)
            return Response<NoDataDTO>.Fail("product not found", 404, false);
        if (product.Stock - quantity < 0)
            return Response<NoDataDTO>.Fail("stock must more than 0", 404, false);


        product.Stock = product.Stock - quantity;
        _genericRepository.Update(product);
        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(200);
    }
    public async Task<Response<NoDataDTO>> ReductionManyProductStock(List<ProductStockChange> productsInfo)
    {
        foreach (var p in productsInfo)
        {
            var product = await _genericRepository.GetByIdAsync(p.productId);
            if (product == null)
                return Response<NoDataDTO>.Fail("product not found", 404, false);
            if (product.Stock - p.quantity < 0)
                return Response<NoDataDTO>.Fail("stock must more than 0", 404, false);


            product.Stock = product.Stock - p.quantity;
            _genericRepository.Update(product);
        }

        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(200);
    }


    public async Task<Response<IEnumerable<ProductDTO>>> WhereWithAtt(Expression<Func<Product, bool>> predicate)
    {
        var list = await _genericRepository.GetWhereWithAtt(predicate).ToListAsync();
        await _genericRepository.CommitAsync();
        return Response<IEnumerable<ProductDTO>>.Success(ObjectMapper.Mapper.Map<IEnumerable<ProductDTO>>(list), 200);
    }

    public async Task<Response<ProductDTO>> AddCheckUrlAsync(ProductDTO entity)
    {
        var url = _genericRepository.GetAll().Any(x => x.Url == entity.Url);
        if (url == true)
            return Response<ProductDTO>.Fail("Url already exists", 400, true);
        var newEntity = ObjectMapper.Mapper.Map<Product>(entity);
        await _genericRepository.AddAsync(newEntity);
        await _genericRepository.CommitAsync();
        var newDto = ObjectMapper.Mapper.Map<ProductDTO>(newEntity);
        return Response<ProductDTO>.Success(newDto, 200);
    }

    public Response<IsUrlDTO> IsUrl(string url)
    {
        var urll = _genericRepository.GetAll().Any(x => x.Url == url);
        _genericRepository.CommitAsync();
        return Response<IsUrlDTO>.Success(new IsUrlDTO
        {
            IsUrl = urll
        }, 200);
    }

    public async Task<Response<NoDataDTO>> ChangeHome(int id, bool isHome)
    {
        var product = await _genericRepository.GetByIdAsync(id);
        product.IsHome = isHome;
        _genericRepository.Update(product);
        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(204);
    }

    public async Task<Response<NoDataDTO>> ChangeApprove(int id, bool isApprove)
    {
        var product = await _genericRepository.GetByIdAsync(id);
        product.IsApprove = isApprove;
        _genericRepository.Update(product);
        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(204);
    }

    public async Task<Response<IEnumerable<ProductDTO>>> GetAllWithCategoriesAndBrandAsync()
    {
        var products = ObjectMapper.Mapper.Map<List<ProductDTO>>(await _genericRepository.GetAllWithCategoriesAndBrands().ToListAsync());
        await _genericRepository.CommitAsync();
        return Response<IEnumerable<ProductDTO>>.Success(products, 200);
    }

    public async Task<Response<ProductDTO>> GetByIdWithCategoriesAsync(int id)
    {
        var product = await _genericRepository.GetByIdWithCategoriesAsync(id);
        await _genericRepository.CommitAsync();
        if (product == null)
        {
            return Response<ProductDTO>.Fail("Id Not Found", 404, true);
        }
        return Response<ProductDTO>.Success(ObjectMapper.Mapper.Map<ProductDTO>(product), 200);
    }

    public async Task<Response<ProductDTO>> GetByIdWithCategoriesAndImagesAsync(int id)
    {
        var product = await _genericRepository.GetByIdWithCategoriesAndImagesAsync(id);
        await _genericRepository.CommitAsync();
        if (product == null)
        {
            return Response<ProductDTO>.Fail("Id Not Found", 404, true);
        }
        return Response<ProductDTO>.Success(ObjectMapper.Mapper.Map<ProductDTO>(product), 200);
    }
    public async Task<Response<ProductDTO>> GetByIdWithAttsAsync(int id)
    {
        var product = await _genericRepository.GetByIdWithAttAsync(id);
        await _genericRepository.CommitAsync();
        if (product == null)
        {
            return Response<ProductDTO>.Fail("Id Not Found", 404, true);
        }
        return Response<ProductDTO>.Success(ObjectMapper.Mapper.Map<ProductDTO>(product), 200);
    }

    public async Task<Response<ProductDTO>> GetByUrlWithAttsAsync(string url)
    {
        var product = await _genericRepository.GetByUrlWithAttAsync(url);
        await _genericRepository.CommitAsync();
        if (product == null)
        {
            return Response<ProductDTO>.Fail("Id Not Found", 404, true);
        }
        return Response<ProductDTO>.Success(ObjectMapper.Mapper.Map<ProductDTO>(product), 200);
    }


    public async Task<Response<Dictionary<string, int>>> GetCountByCategory(string categoryUrl)
    {
        var count = await _genericRepository.GetWhere(x => x.Category.Url == categoryUrl).CountAsync();
        await _genericRepository.CommitAsync();
        var result = new Dictionary<string, int>() { { "data", count } };
        return Response<Dictionary<string, int>>.Success(result, 200);
    }
    public async Task<Response<ProductDTOAndTotalCount>> WherePage(string? category, int page, int pageSize, int sort, Expression<Func<Product, bool>>? predicate = null)
    {
        var products = ObjectMapper.Mapper.Map<List<ProductDTO>>(await _genericRepository.WherePage(page, pageSize, sort, predicate).ToListAsync());
        var count = await _genericRepository.GetCountByCategory(predicate);


        IQueryable<Product> productss;
        if (category != null)
            productss = _genericRepository.GetAll().Where(x => x.MainCategory.Url == category);
        else
            productss = _genericRepository.GetAll();
        double? mostPrice = null;
        if (products.Count > 0) mostPrice = await productss.MaxAsync(x => x.Price);

        await _genericRepository.CommitAsync();
        return Response<ProductDTOAndTotalCount>.Success(new ProductDTOAndTotalCount { Product = products, TotalCount = count, MaxPrice = mostPrice }, 200);
    }


    public async Task<Response<IEnumerable<ProductDTO>>> WhereWithCategories(Expression<Func<Product, bool>> predicate)
    {
        var products = ObjectMapper.Mapper.Map<List<ProductDTO>>(await _genericRepository.WhereWithCategories(predicate).ToListAsync());
        await _genericRepository.CommitAsync();
        return Response<IEnumerable<ProductDTO>>.Success(products, 200);
    }

}
