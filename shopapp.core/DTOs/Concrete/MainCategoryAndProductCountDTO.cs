using shopapp.core.DTOs.Abstract;

namespace shopapp.core.DTOs.Concrete;

public class MainCategoryAndProductCountDTO
{
    public MainCategoryAndProductCountDTO()
    {
        this.Categories = new List<CategoryDTO>();
    }

    public int ProductCount { get; set; }

    public List<CategoryDTO> Categories { get; set; }
}
