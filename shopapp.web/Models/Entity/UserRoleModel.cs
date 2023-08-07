using Microsoft.AspNetCore.Identity;

namespace shopapp.web.Models.Entity
{
    public class UserRoleModel : IdentityRole
    {
        public UserRoleModel() : base() { }
        public UserRoleModel(string name) : base(roleName: name) { }
    }
}
