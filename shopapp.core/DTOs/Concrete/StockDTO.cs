using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.DTOs.Concrete
{
	public class StockDTO:IDTO
	{
		public StockDTO()
		{
			this.StockValues = new List<StockValueDTO>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public int ProductId { get; set; }
		public ProductDTO Product { get; set; }

		public List<StockValueDTO> StockValues { get; set; }
	}
}
