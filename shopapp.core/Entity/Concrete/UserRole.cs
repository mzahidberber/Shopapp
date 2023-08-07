using Microsoft.AspNetCore.Identity;

namespace shopapp.core.Entity.Concrete
{
    public class UserRole : IdentityRole
    {
        public UserRole() : base() { }
        public UserRole(string name) : base(roleName: name) { }
    }
}
