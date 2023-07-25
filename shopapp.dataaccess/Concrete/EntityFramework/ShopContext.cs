using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using shopapp.core.Entity.Concrete;
using shopapp.dataaccess.Concrete.EntityFramework.Mapping;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    public class ShopContext:IdentityDbContext<User,UserRole,string>
    {
        public IConfiguration _configuration { get; set; }
        public ShopContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        //public ShopContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfiguration(new UserMapping());
            //builder.ApplyConfiguration(new UserRoleMapping());
            //builder.ApplyConfiguration(new CategoryMapping());
            //builder.ApplyConfiguration(new ProductMapping());
            builder.ApplyConfiguration(new ProductCategoryMapping());
            builder.ApplyConfiguration(new OrderMapping());
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
                // Production ortamında yapılacak işlemler
            }
            else
            {
                // Diğer ortamlarda yapılacak varsayılan işlemler
            }
            
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }



    }
}
