using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using shopapp.core.Entity.Concrete;
using shopapp.dataaccess.Concrete.EntityFramework.Mapping;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    public class ShopContext : IdentityDbContext<User, UserRole, string>
    {
        public IConfiguration _configuration { get; set; }
        public ShopContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SubCategoryFeature> SubCategoryFeatures { get; set; }
        public DbSet<SubCategoryFeatureValue> SubCategoryFeatureValues { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserRoleMapping());
            builder.ApplyConfiguration(new UserMapping(_configuration));
            builder.ApplyConfiguration(new MainCategoryMapping());
            builder.ApplyConfiguration(new CategoryMapping());
            builder.ApplyConfiguration(new SubCategoryMapping());
            builder.ApplyConfiguration(new SubCategoryFeatureMapping());
            builder.ApplyConfiguration(new SubCategoryFeatureValueMapping());
            builder.ApplyConfiguration(new BrandMapping());
            builder.ApplyConfiguration(new ProductMapping());
            builder.ApplyConfiguration(new OrderMapping());
            builder.ApplyConfiguration(new OrderItemMapping());
            builder.ApplyConfiguration(new CartItemMapping());
            builder.ApplyConfiguration(new ImageMapping());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var connectionString = "";
            if (environment == "Development")
            {
                connectionString = _configuration.GetConnectionString("shopdb");
            }
            else if (environment == "Production")
            {
                var host=Environment.GetEnvironmentVariable("dbHost");
                var name=Environment.GetEnvironmentVariable("dbName");
                var password=Environment.GetEnvironmentVariable("dbPassword");
                var port=Environment.GetEnvironmentVariable("dbPort");
                connectionString = $"server={host};port={port};database={name};User Id=root;password={password};";
               
            }
            else
            {
                // Diğer ortamlarda yapılacak işlemler
            }
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            //optionsBuilder.UseSqlite("Data Source=shopdb.db;");
        }



    }
}
