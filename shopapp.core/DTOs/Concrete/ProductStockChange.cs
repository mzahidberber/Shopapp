using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shopapp.core.DTOs.Concrete;

public class ProductStockChange
{
    [JsonIgnore]
    public int productId { get; set; }
	[JsonIgnore]
	public int quantity { get; set; }
}
