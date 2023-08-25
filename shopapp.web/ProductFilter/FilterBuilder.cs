using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.web.ProductFilter;

public class FilterBuilder
{
    List<Expression<Func<Product, bool>>> ExpressionList { get; set; } = new();

    public FilterBuilder AddFilter(IFilter filter)
    {
        ExpressionList.Add(filter.Expression());
        return this;
    }

    public Expression<Func<Product, bool>>? Build()
    {
        var parameter = Expression.Parameter(typeof(Product), "p");
        Expression? combinedBody = null;
        
        if(ExpressionList.Count > 0)
        {
            foreach (var condition in ExpressionList)
            {
                var invokedCondition = Expression.Invoke(condition, parameter);
                combinedBody = combinedBody == null
                    ? invokedCondition
                    : Expression.AndAlso(combinedBody, invokedCondition);
            }
        }
        
        return combinedBody!=null?Expression.Lambda<Func<Product, bool>>(combinedBody, parameter):null;
    }
}
