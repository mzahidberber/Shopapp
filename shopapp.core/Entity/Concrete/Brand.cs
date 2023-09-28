using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete;

public class Brand : IEntity
{
    public Brand()
    {
        this.Products = new List<Product>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }

    public List<Product> Products { get; set; }
}
