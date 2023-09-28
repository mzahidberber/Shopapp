using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete;

public class ImageService : GenericService<Image, ImageDTO>, IImageService
{
    public IProductRepository _productRepository { get; set; }
    public IImageRepository _genericRepository { get; set; }
    public ImageService(IImageRepository genericRepository, IProductRepository productRepository) : base(genericRepository)
    {
        _genericRepository = genericRepository;
        _productRepository = productRepository;
    }

    public async Task<Response<NoDataDTO>> SyncProductImages(List<string> urls, int productId)
    {
        var productImages = _genericRepository.GetWhere(x => x.ProductId == productId);
        foreach (var image in productImages)
        {
            if (!urls.Any(x => x == image.Url))
            {
                _genericRepository.Delete(ObjectMapper.Mapper.Map<Image>(image));
            }
        }
        foreach (var url in urls)
        {
            if (!productImages.Any(x => x.Url == url))
            {
                await _genericRepository.AddAsync(new Image
                {
                    Url = url,
                    ProductId = productId
                });
            }
        }
        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(204);
    }
    public async Task<Response<NoDataDTO>> RemoveForName(string name)
    {
        var isExistEntity = await _genericRepository.GetWhere(x => x.Url == name).FirstOrDefaultAsync();
        if (isExistEntity == null)
        {
            return Response<NoDataDTO>.Fail("Id Not Found", 404, true);

        }
        _genericRepository.Delete(isExistEntity);
        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(204);
    }
}
