using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete;

public class MainCategory: IEntity
{
    public MainCategory()
    {
        this.Categories = new List<Category>();
        this.Products = new List<Product>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public List<Product> Products { get; set; }

    public List<Category> Categories { get; set; }
}
