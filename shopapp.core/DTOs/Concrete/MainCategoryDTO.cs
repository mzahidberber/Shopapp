using shopapp.core.DTOs.Abstract;

namespace shopapp.core.DTOs.Concrete;

public class MainCategoryDTO:IDTO
{
    public MainCategoryDTO()
    {
        this.Categories = new List<CategoryDTO>();
        this.Products = new List<ProductDTO>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public List<ProductDTO> Products { get; set; }

    public List<CategoryDTO> Categories { get; set; }
}
