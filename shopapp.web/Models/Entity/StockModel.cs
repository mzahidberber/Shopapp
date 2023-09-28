namespace shopapp.web.Models.Entity;

public class StockModel
{
    public StockModel()
    {
        this.StockValues = new List<StockValueModel>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }

    public List<StockValueModel> StockValues { get; set; }
}
