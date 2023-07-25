using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.business.Concrete
{
    public class ProductService : GenericService<Product, ProductDTO>,IProductService
    {
        private readonly IProductRepository _genericRepository;
        public ProductService(IProductRepository genericRepository) : base(genericRepository)
        {
            this._genericRepository = genericRepository;
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

        public async Task<Response<IEnumerable<ProductDTO>>> WhereWithCategories(Expression<Func<Product, bool>> predicate)
        {
            var products = ObjectMapper.Mapper.Map<List<ProductDTO>>(await _genericRepository.WhereWithCategories(predicate).ToListAsync());
            await _genericRepository.CommitAsync();
            return Response<IEnumerable<ProductDTO>>.Success(products, 200);
        }
    }
}
