using shopapp.core.DTOs.Abstract;

namespace shopapp.core.DTOs.Concrete;

public class ImageDTO:IDTO
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int ProductId { get; set; }
    public ProductDTO Product { get; set; }
}
