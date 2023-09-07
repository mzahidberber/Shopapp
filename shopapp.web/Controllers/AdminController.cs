using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.CrossCuttingConcers.Caching;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.core.Reflection;
using shopapp.web.Helpers;
using shopapp.web.Mapper;
using shopapp.web.Models.Account;
using shopapp.web.Models.Admin;
using shopapp.web.Models.Entity;
using System.Data;

namespace shopapp.web.Controllers;

[Authorize(Roles = "Admin")]
[LogAspectController]
public class AdminController : Controller
{
    private RoleManager<UserRole> _roleManager { get; set; }
    private UserManager<User> _userManager { get; set; }
    public IProductService _productService { get; set; }
    public ICategoryService _categoryService { get; set; }
    public IMainCategoryService _mainCategoryService { get; set; }
    public IImageService _imageService { get; set; }
    public ISubCategoryFeatureValueService _subCategoryFeatureValueService { get; set; }
    public IConfiguration _configuration { get; set; }

    public ICacheManager _cacheManager { get; set; }

    private IWebHostEnvironment _webHostEnvironment { get; set; }

    public AdminController(RoleManager<UserRole> roleManager, UserManager<User> userManager, IProductService productService, ICategoryService categoryService, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, ICacheManager cacheManager, IMainCategoryService mainCategoryService, IImageService imageService, ISubCategoryFeatureValueService subCategoryFeatureValueService)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _productService = productService;
        _categoryService = categoryService;
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
        _cacheManager = cacheManager;
        _mainCategoryService = mainCategoryService;
        _imageService = imageService;
        _subCategoryFeatureValueService = subCategoryFeatureValueService;
    }

    public async Task<IEnumerable<MainCategoryModel>> GetCategoriesAsync()
    {
        var key = Reflection.CreateCacheKey(typeof(AdminController), "GetCategoriesAsync");
        if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IEnumerable<MainCategoryModel>>(key);
        var categories = await _mainCategoryService.GetAllWithCategoriAndSubCategoriesAndBrands();
        var categoriModel = ObjectMapper.Mapper.Map<IEnumerable<MainCategoryModel>>(categories.data);
        _cacheManager.Add(key, categoriModel, 60);
        return categoriModel;
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
            return Redirect("UserList");
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
        //return View();
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
        var product = await _productService.GetAllAsync();
        return View(ObjectMapper.Mapper.Map<List<ProductModel>>(product.data));
    }


	public IActionResult GetPartialView(string partialViewName)
	{
		return PartialView(partialViewName);
	}

    [HttpGet]
    public async Task<IActionResult> ProductCreate()
    {
        ViewBag.Categories =await GetCategoriesAsync();
        return PartialView(new ProductModel());
    }

    [HttpPost]
    public async Task<IActionResult> FilterCategoryAndBrands(int mainCategoryId,int? categoryId=null,int? subCategoryId=null)
    {
        var categories= await GetCategoriesAsync();
        if(categoryId!=null && subCategoryId!=null){
            return Json(categories.FirstOrDefault(x => x.Id == mainCategoryId).Categories.FirstOrDefault(x=>x.Id==categoryId).SubCategories.FirstOrDefault(x=>x.Id==subCategoryId));
        }
        if(categoryId!=null){
            return Json(categories.FirstOrDefault(x => x.Id == mainCategoryId).Categories.FirstOrDefault(x=>x.Id==categoryId));
        }
        return Json(categories.FirstOrDefault(x => x.Id == mainCategoryId));
    }


    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductModel product, List<string> imageUrls, List<string>? features=null,List<string>? values=null)
    {
        ViewBag.Categories =await GetCategoriesAsync();
        if (ModelState.IsValid)
        {
            foreach (var imageUrl in imageUrls)
            {
                product.Images.Add(new ImageModel
                {
                    Url= imageUrl,
                    ProductId=product.Id
                });
                
            }
            if (_productService.IsUrl(product.Url).data.IsUrl)
                return View(product);


            var ct = await GetCategoriesAsync();
            var sub = ct.FirstOrDefault(x => x.Id == product.MainCategoryId).Categories.FirstOrDefault(x => x.Id == product.CategoryId).SubCategories.FirstOrDefault(x => x.Id == product.SubCategoryId);
            for (int i = 0; i < features.Count; i++)
            {
                product.SubCategoryFeatureValues.Add(new SubCategoryFeatureValueModel
                {
                    Value = values[i],
                    SubCategoryFeatureId= sub.SubCategoryFeatures.FirstOrDefault(x => features[i]==x.Name).Id
                });
            }
            
            await this._productService.AddAsync(ObjectMapper.Mapper.Map<ProductDTO>(product));
            return RedirectToAction("ProductList");
        }
        return PartialView(product);
    }
    public IActionResult BrowseImages()
    {
        var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), _webHostEnvironment.WebRootPath, "img"));
        ViewBag.fileInfo = dir.GetFiles();
        return PartialView("FileExplorer");
    }
    
    [HttpGet]
    public async Task<IActionResult> ProductEdit(int id)
    {
        ViewBag.Categories =await GetCategoriesAsync();
        var product = this._productService.GetByIdWithAttsAsync(id).Result.data;
        return View(ObjectMapper.Mapper.Map<ProductModel>(product));
    }

    [HttpPost]
    public async Task<IActionResult> LoadImages(int productId){
        var images =await _imageService.Where(x => x.ProductId == productId);
        return Json(images.data);
    }


    [HttpPost]
    public async Task<IActionResult> ProductEdit(ProductModel product, List<string> imageUrls, List<string>? features = null, List<string>? values = null)
    {
        ViewBag.Categories = await GetCategoriesAsync();
        if (ModelState.IsValid)
        {
            //var oldProduct=await _productService.GetByIdAsync(product.Id);
            //if (_productService.IsUrl(product.Url).data.IsUrl && oldProduct.data.Url!=product.Url)
            //    return View(product);


            await _subCategoryFeatureValueService.SyncProductFeatures(product.Id, product.SubCategoryId, features, values);

            await _imageService.SyncProductImages(imageUrls, product.Id);

            await this._productService.Update(ObjectMapper.Mapper.Map<ProductDTO>(product), product.Id);


        }
            
        return RedirectToAction("ProductList");
    }
    [HttpPost]
    public async Task<IActionResult> ProductDelete(int productId)
    {
        await _productService.Remove(productId);

        return Redirect("ProductList");
    }
    [HttpPost]
    public IActionResult IsUrl(string url) => Json(_productService.IsUrl(url).data.IsUrl);
    
    public IActionResult ImageList()
    {
        var images = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img"));
        return View(images.Select(x => x.Split("\\").Last()).ToList());
    }

    public IActionResult ImagesUrls()
    {
        var images = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img"));
        return Json(images.Select(x => x.Split("\\").Last()).ToList());
    }
    public async Task<IActionResult> IsHomeChange(bool isHome, int productId)
    {
        var result=await _productService.ChangeHome(productId, isHome);
        return Json(result.statusCode);
    }
    public async Task<IActionResult> IsApproveChange(bool isApprove, int productId)
    {
        var result=await _productService.ChangeApprove(productId, isApprove);
        return Json(result.statusCode);
    }
    //public async Task<IActionResult> IsHomeChange(bool  isHome,int productId)
    //{
    //    await _productService.ChangeHome(productId, isHome);
    //    return Redirect("ProductList");
    //}

    //public async Task<IActionResult> IsApproveChange(bool isApprove, int productId)
    //{
    //    await _productService.ChangeApprove(productId, isApprove);
    //    return Redirect("ProductList");
    //}


    [HttpPost]
    public async Task<IActionResult> AddImage(List<IFormFile> files,string name)
    {
        if (files != null)
        {
            name= name.Replace(" ", "-");
            var images = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img"));
            var imageNames = images.Select(x => x.Split("\\").Last()).ToList();
            if (imageNames.Any(x=>x.Contains(name)))
                name = string.Format($"{name}-{Guid.NewGuid()}");
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{name}-{files.IndexOf(file)}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

        }
        return Redirect("ImageList");
    }

    public async Task<IActionResult> DeleteImage(string name)
    {
        var products=await _productService.Where(x=>x.Images.Any(x=>x.Url== name) || x.HomeImageUrl==name);
        if(products.data.Count()>0)
            return Redirect("ImageList");
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", name);
        System.IO.File.Delete(path);
        return Redirect("ImageList");
    }


    [HttpPost]
    public async Task<IActionResult> CheckImageProduct(string name)
    {
        var products=await _productService.Where(x=>x.Images.Any(x=>x.Url== name) || x.HomeImageUrl==name);
        return Json(products.data);
    }

    public async Task<IActionResult> CategoriesList()
    {
        var categories=await _mainCategoryService.GetAllWithCategoriAndSubCategoriesAndBrands();
        return View(ObjectMapper.Mapper.Map<List<MainCategoryModel>>(categories.data));
    }


}
