using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.DTOs.Concrete;

public class CategoryDTO : IDTO
{
	public CategoryDTO()
	{
		this.Products = new List<ProductDTO>();
		this.SubCategories = new List<SubCategoryDTO>();
	}
	public int Id { get; set; }
	public string Name { get; set; }
	public string Url { get; set; }

	public int MainCategoryId { get; set; }
	public MainCategoryDTO MainCategory { get; set; }

	public List<ProductDTO> Products { get; set; }
	public List<SubCategoryDTO> SubCategories { get; set; }
}
