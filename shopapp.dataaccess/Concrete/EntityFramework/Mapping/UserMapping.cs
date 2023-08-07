using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping
{
    internal class UserMapping : IEntityTypeConfiguration<User>
    {
        private IConfiguration _configuration { get; set; }
        public UserMapping(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            builder.HasData(
                new User
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = _configuration["Data:AdminUser:username"],
                    Email = _configuration["Data:AdminUser:email"],
                    NormalizedUserName = _configuration["Data:AdminUser:username"].ToUpper(),
                    NormalizedEmail = _configuration["Data:AdminUser:email"].ToUpper(),
                    PasswordHash = hasher.HashPassword(null, _configuration["Data:AdminUser:password"]),
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                });
        }
    }
}
