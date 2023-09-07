using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract;

public interface IImageService:IGenericService<Image, ImageDTO>
{
    Task<Response<NoDataDTO>> RemoveForName(string name);
    Task<Response<NoDataDTO>> SyncProductImages(List<string> urls, int productId);
}
