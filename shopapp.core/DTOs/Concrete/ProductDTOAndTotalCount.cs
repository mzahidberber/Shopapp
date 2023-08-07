namespace shopapp.core.DTOs.Concrete
{
    public class ProductDTOAndTotalCount
    {
        public IEnumerable<ProductDTO> Product { get; set; }
        public int TotalCount { get; set; }
    }
}
