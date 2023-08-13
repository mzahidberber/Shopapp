using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete
{
    public class Product : IEntity
    {
        public Product()
        {
            this.Images = new List<Image>();
            this.SubCategoryFeatureValues = new List<SubCategoryFeatureValue>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string HomeImageUrl { get; set; }
        public int Stock { get; set; }
        public bool IsApprove { get; set; }
        public bool IsHome { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

		public int SubCategoryId { get; set; }
		public SubCategory SubCategory { get; set; }

		public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }

        public List<Image> Images { get; set; }

        public List<SubCategoryFeatureValue> SubCategoryFeatureValues { get; set; }
    }
}
