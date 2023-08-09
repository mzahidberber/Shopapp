using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.business.Concrete.ProductFilter;
internal interface IFilter
{

}
internal class CategoryFilter:IFilter
{

    
}
internal class ProductFilter
{
    public string[] Categories { get; set; }
    public string? Price { get; set; }

    public ProductFilter(string[] categories, string? price)
    {
        Categories = categories;
        Price = price;
    }
    public bool CategoryFilter(Product p)
    {
        return p.ProductCategories.Any(pr => this.Categories.Any(x => x == pr.Category.Url));
    }

    public bool PriceFilter(Product p)
    {
        var prcList = this.Price.Split("-");
        return p.Price <= Convert.ToDouble(prcList[1]) && p.Price >= Convert.ToDouble(prcList[0]);
    }


    public Expression<Func<Product,bool>> CreateSearch()
    {
        return x => CategoryFilter(x) && CategoryFilter(x);
    }
}
