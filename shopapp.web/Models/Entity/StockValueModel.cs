namespace shopapp.web.Models.Entity;

public class StockValueModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
    public int StockId { get; set; }
    public StockModel Stock { get; set; }
}
