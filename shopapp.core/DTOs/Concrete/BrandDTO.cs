using shopapp.core.DTOs.Abstract;

namespace shopapp.core.DTOs.Concrete;

public class BrandDTO:IDTO
{
    public BrandDTO()
    {
        this.Products = new List<ProductDTO>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public int CategoryId { get; set; }
    public CategoryDTO Category { get; set; }

    public List<ProductDTO> Products { get; set; }
}
