using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete;

public class StockValue : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
    public int StockId { get; set; }
    public Stock Stock { get; set; }
}
