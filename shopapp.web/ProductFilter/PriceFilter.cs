using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.web.ProductFilter
{
    public class PriceFilter : IFilter
    {
        public string Price { get; set; }
        public PriceFilter(string priceFilter)
        {
            Price = priceFilter;
        }
        public Expression<Func<Product, bool>> Expression()
        {
            var prcList = Price.Split("-");
            if (prcList[1] != "")
                return p => p.Price <= Convert.ToDouble(prcList[1]) && p.Price >= Convert.ToDouble(prcList[0]);

            else
                return p => p.Price >= Convert.ToDouble(prcList[0]);
        }
    }
}
