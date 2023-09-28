using System.Text.Json.Serialization;

namespace shopapp.core.DTOs.Concrete;

public class ProductStockChange
{
    [JsonIgnore]
    public int productId { get; set; }
    [JsonIgnore]
    public int quantity { get; set; }
}
