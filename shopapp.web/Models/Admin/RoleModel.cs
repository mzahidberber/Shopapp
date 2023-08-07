using Microsoft.AspNetCore.Identity;
using shopapp.web.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models.Admin;

public class RoleModel
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
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
