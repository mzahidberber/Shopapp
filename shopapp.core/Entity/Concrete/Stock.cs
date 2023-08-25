using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete;

public class Stock:IEntity
{
    public Stock()
    {
        this.StockValues = new List<StockValue>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public List<StockValue> StockValues { get; set; }
}
