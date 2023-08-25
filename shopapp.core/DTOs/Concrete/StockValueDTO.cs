using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.DTOs.Concrete;

public class StockValueDTO:IDTO
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int Value { get; set; }
	public int StockId { get; set; }
	public StockDTO Stock { get; set; }
}
