using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.web.ProductFilter
{
    public class IsApprove : IFilter
    {
        public bool IsApproveBool { get; set; }
        public IsApprove(bool isApprove)
        {
            this.IsApproveBool = isApprove;
        }
        public Expression<Func<Product, bool>> Expression()
        {
            return x => x.IsApprove == this.IsApproveBool;
        }
    }
}
