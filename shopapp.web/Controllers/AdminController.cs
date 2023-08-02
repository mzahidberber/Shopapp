using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopapp.core.Aspects.Logging;
using shopapp.core.Entity.Concrete;
using shopapp.web.Mapper;
using shopapp.web.Models;
using shopapp.web.Models.Entity;
using System.Runtime.Serialization;
using System.Text.Json;

namespace shopapp.web.Controllers
{
    [Authorize(Roles ="Admin")]
    [LogAspectController]
    public class AdminController:Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RoleManager<UserRole> _roleManager;
        private UserManager<User> _userManager;

		public AdminController(ILogger<HomeController> logger, RoleManager<UserRole> roleManager, UserManager<User> userManager)
		{
			_logger = logger;
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public IActionResult Index()
        {
            return View();
        }


		public async Task<IActionResult> RoleList()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

		public async Task<IActionResult> RoleCreate()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> RoleCreate(RoleModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _roleManager.CreateAsync(new UserRole(model.Name));
				if(result.Succeeded)
				{
					return RedirectToAction("RoleList");
				}
				else
				{
                    foreach (var error in result.Errors)
                    {
						ModelState.AddModelError("", error.Description);
                    }
                }
			}
			return View(model);
		}

		public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonmembers = new List<User>();
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var list=await _userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = ObjectMapper.Mapper.Map<List<UserModel>>(members),
                NonMembers = ObjectMapper.Mapper.Map<List<UserModel>>(nonmembers)
			};
            return View(model);
        }
        [HttpPost]
		public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            return Redirect("/admin/role/" + model.RoleId);
        }

		public async Task<IActionResult> UserList()
        {
            return View(await _userManager.Users.ToListAsync());
        }

		public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                var selectedRoles=await _userManager.GetRolesAsync(user);
                var roles=await _roleManager.Roles.Select(x => x.Name).ToListAsync();
                ViewBag.Roles = roles;
                return View(new UserDetailModel()
                {
                    UserId=user.Id,
                    UserName=user.UserName,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    Email=user.Email,
                    EmailConfirmed=user.EmailConfirmed,
                    SelectedRoles=selectedRoles

                });
            }
            return Redirect("~/admin/user/list");
        }
        [HttpPost]
		public async Task<IActionResult> UserEdit(UserDetailModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
                ViewBag.Roles = roles;
                var user = await _userManager.FindByIdAsync(model.UserId);
                if(user != null )
                {
                    user.FirstName=model.FirstName;
                    user.LastName=model.LastName;
                    user.UserName=model.UserName;
                    user.Email=model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result= await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRoles=await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user,userRoles.Except(selectedRoles).ToArray<string>());

                        return Redirect("~/admin/user/list");
                    }
                }
                return Redirect("~/admin/user/list");
            }
            
            return View(model);
        }
    }
}
