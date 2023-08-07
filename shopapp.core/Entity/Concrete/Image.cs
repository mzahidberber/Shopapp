using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete;

public class Image : IEntity
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}
