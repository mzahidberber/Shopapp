namespace shopapp.web.Models.Entity;

public class ImageModel
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }
}
