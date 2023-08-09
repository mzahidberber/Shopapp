using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.web.Helpers;
using shopapp.web.Mapper;
using shopapp.web.Models.Account;
using shopapp.web.Models.Admin;
using shopapp.web.Models.Entity;
using shopapp.web.ViewModels;
using System.Data;
using System.Drawing.Printing;

namespace shopapp.web.Controllers
{
    [Authorize(Roles = "Admin")]
    [LogAspectController]
    public class AdminController : Controller
    {
        private RoleManager<UserRole> _roleManager { get; set; }
        private UserManager<User> _userManager { get; set; }
        public IProductService _productService { get; set; }
        public ICategoryService _categoryService { get; set; }
        public IConfiguration _configuration { get; set; }

        public AdminController(RoleManager<UserRole> roleManager, UserManager<User> userManager, IProductService productService, ICategoryService categoryService, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _productService = productService;
            _categoryService = categoryService;
            _configuration = configuration;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> RoleList() => View(ObjectMapper.Mapper.Map<List<UserRoleModel>>(await _roleManager.Roles.ToListAsync()));

        [HttpPost]
        public async Task<IActionResult> RoleCreate(string Name)
        {
            if (Name != null && Name != "")
            {
                var result = await _roleManager.CreateAsync(new UserRole(Name));
                if (result.Succeeded)
                    TempDataMessage.CreateMessage(TempData, key: "message", message: "Role Added.");
                else
                    TempDataMessage.CreateMessage(TempData,
                        key: "message", message: string.Join(",", result.Errors.Select(x => x.Description)),
                        alertType: "warning");

                return RedirectToAction("RoleList");

            }
            TempDataMessage.CreateMessage(TempData,
                        key: "message", message: "Name isn't be empty",
                        alertType: "warning");
            return RedirectToAction("RoleList");
        }

        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<UserModel>();
            var nonmembers = new List<UserModel>();
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(ObjectMapper.Mapper.Map<UserModel>(user));
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
                            TempDataMessage.CreateMessage(TempData,
                                key: "message", message: string.Join(",", result.Errors.Select(x => x.Description)),
                                alertType: "warning");
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
                            TempDataMessage.CreateMessage(TempData,
                                key: "message", message: string.Join(",", result.Errors.Select(x => x.Description)),
                                alertType: "warning");
                        }
                    }
                }
            }
            return Redirect("/admin/role/" + model.RoleId);
        }

        [HttpPost]
        public async Task<IActionResult> RoleDelete(string roleId)
        {
            if (roleId != null && roleId != string.Empty)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    TempDataMessage.CreateMessage(TempData,
                        key: "message", message: "Role not found.",
                        alertType: "warning");
                    return RedirectToAction("RoleList");
                }

                else
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                        TempDataMessage.CreateMessage(TempData,
                            key: "message", message: "Role deleted.");
                    else
                        TempDataMessage.CreateMessage(TempData,
                            key: "message", message: string.Join(",", result.Errors.Select(x => x.Description)),
                            alertType: "warning");
                }

            }
            return RedirectToAction("RoleList");
        }

        public async Task<IActionResult> UserList()
        {
            return View(ObjectMapper.Mapper.Map<List<UserModel>>(await _userManager.Users.ToListAsync()));
        }


        public IActionResult UserCreate() => View(new RegisterModel());

        [HttpPost]
        public async Task<IActionResult> UserCreate(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                TempDataMessage.CreateMessage(TempData, key: "message", message: "User created.");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempDataMessage.CreateMessage(TempData,
                    key: "message", message: string.Join(",", result.Errors.Select(x => x.Description)),
                    alertType: "warning");
            }

            return View(model);
        }

        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
                ViewBag.Roles = roles;
                return View(new UserDetailModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles

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
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

                        return Redirect("~/admin/user/list");
                    }
                }
                return Redirect("~/admin/user/list");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserDelete(string UserId)
        {
            if (UserId != null && UserId != string.Empty)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    TempDataMessage.CreateMessage(TempData,
                        key: "message", message: "User not found.",
                        alertType: "warning");
                    return RedirectToAction("RoleList");
                }

                else
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                        TempDataMessage.CreateMessage(TempData,
                            key: "message", message: "User deleted.");
                    else
                        TempDataMessage.CreateMessage(TempData,
                            key: "message", message: string.Join(",", result.Errors.Select(x => x.Description)),
                            alertType: "warning");
                }

            }
            return RedirectToAction("UserList");
        }


        public async Task<IActionResult> ProductList(string? category = null, int page = 1)
        {
            return View();
            //if (RouteData.Values["action"].ToString() == "ProductList")
            //{
            //    ViewBag.SelectedCategory = RouteData?.Values["id"];
            //}
            //var product = new ProductDTOAndTotalCount();
            //var pageSize = Convert.ToInt32(_configuration["PageSetting:PageSize"]);
            //if (category == null) product = this._productService.WherePage(page, pageSize).Result.data;
            //else product = this._productService.WherePage(page, pageSize, x => x.ProductCategories.Any(x => x.Category.Url == category)).Result.data;
            //ViewBag.PageInfo = new PageInfo
            //{
            //    Url = "/admin/product",
            //    TotalItems = product.TotalCount,
            //    CurrentPage = page,
            //    ItemsPerPage = pageSize,
            //    CurrentCategory = category
            //};
            //ViewBag.Categories= ObjectMapper.Mapper.Map<List<CategoryModel>>(this._categoryService.GetAllAsync().Result.data);
            //return View(ObjectMapper.Mapper.Map<List<ProductModel>>(product.Product));
        }


    }
}
