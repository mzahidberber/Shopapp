using Microsoft.AspNetCore.Identity;
using shopapp.core.Entity.Concrete;
using shopapp.web.Models.Entity;

namespace shopapp.web.Models
{
	public class RoleModel
	{
		public string Name { get; set; } = null!;
    }

	public class RoleDetails
    {
		public IdentityRole Role { get; set; }
		public IEnumerable<UserModel> Members { get; set; }
		public IEnumerable<UserModel> NonMembers { get; set; }
    }

	public class RoleEditModel
    {
		public string RoleId { get; set; }
		public string RoleName { get; set; }
		public string[]? IdsToAdd { get; set; }
		public string[]? IdsToDelete { get; set; }
    }
}
