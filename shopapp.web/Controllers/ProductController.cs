using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using shopapp.core.Aspects.Caching;
using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.CrossCuttingConcers.Caching;
using shopapp.core.CrossCuttingConcers.Caching.Microsoft;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Reflection;
using shopapp.web.Mapper;
using shopapp.web.Models.Entity;
using shopapp.web.Models.ProductModels;
using shopapp.web.ProductFilter;
using shopapp.web.ViewModels;

namespace shopapp.web.Controllers;

[LogAspectController]
public class ProductController : Controller
{
    private IProductService _productService { get; set; }
    private ICategoryService _categoryService { get; set; }
    private IMainCategoryService _mainCategoryService { get; set; }
    private IConfiguration _configuration { get; set; }
    public ICacheManager _cacheManager { get; set; }

    public ProductController(IProductService productService, ICategoryService categoryService, IConfiguration configuration, ICacheManager cacheManager, IMainCategoryService mainCategoryService)
    {
        this._productService = productService;
        this._categoryService = categoryService;
        this._configuration = configuration;
        this._cacheManager = cacheManager;
        this._mainCategoryService = mainCategoryService;
    }

    public async Task<IEnumerable<MainCategoryModel>> GetCategoriesAsync()
    {
        var key = Reflection.CreateCacheKey(typeof(ProductController), "GetCategoriesAsync");
        if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IEnumerable<MainCategoryModel>>(key);
        var categories =await _mainCategoryService.GetAllWithCategoriAndSubCategoriesAndBrands();
        var categoriModel = ObjectMapper.Mapper.Map<IEnumerable<MainCategoryModel>>(categories.data);
        _cacheManager.Add(key, categoriModel, 60);
        return categoriModel;
    }


    //[CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 60)]
    public async Task<IActionResult> Details(string url)
    {
        //var key = Reflection.CreateCacheKey(typeof(ProductController), "Details", id);
        //if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IActionResult>(key);



        var product = await this._productService.GetByUrlWithAttsAsync(url);
        if (product.statusCode == 200)
            return View(ObjectMapper.Mapper.Map<ProductModel>(product.data));
        else
            return View("Error");
    }


    public async Task<SelectedCategoryModel> FindCategoryStatusAsync(string category)
    {
        var categoryModels = await GetCategoriesAsync();
        var main = categoryModels.FirstOrDefault(x => x.Url == category);
        var cate = categoryModels.FirstOrDefault(x => x.Categories.Any(x => x.Url == category));
        var sub = categoryModels.FirstOrDefault(x => x.Categories.Any(x => x.SubCategories.Any(x => x.Url == category)));
        
        MainCategoryModel? SelectedMainCategory = null;
        CategoryModel? SelectedCategory = null;
        SubCategoryModel? SelectedSubCategory = null;
        IEnumerable<BrandModel>? SelectedBrands = null;
        int categoryType;



        if (main != null)
        {
            categoryType = 1;
            SelectedMainCategory = main;
            SelectedBrands = SelectedMainCategory.Categories.SelectMany(x => x.SubCategories.SelectMany(x => x.Brands));
        }
        else if (cate != null)
        {
            SelectedMainCategory = cate;
            SelectedCategory = SelectedMainCategory.Categories.FirstOrDefault(x => x.Url == category);
            SelectedBrands = SelectedCategory.SubCategories.SelectMany(x => x.Brands);
            categoryType = 2;
        }
        else if (sub != null)
        {
            categoryType = 3;
            SelectedMainCategory = sub;
            SelectedCategory = SelectedMainCategory.Categories.FirstOrDefault(x => x.SubCategories.Any(x => x.Url == category));
            SelectedSubCategory = SelectedCategory.SubCategories.FirstOrDefault(x => x.Url == category);
            SelectedBrands = SelectedSubCategory.Brands;
        }
        else categoryType = 0;

        return new SelectedCategoryModel
        {
            MainCategory=SelectedMainCategory,
            CategoryType=categoryType,
            Category=SelectedCategory,
            SubCategory=SelectedSubCategory,
            Brands=SelectedBrands
        };
    }

    //[CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 60)]
    public async Task<IActionResult> Index(string? category,List<string> c, List<string> b, string? prc=null,int s=1, int p = 1)
    {
        //var key = Reflection.CreateCacheKey(typeof(ProductController), "List",c, prc ?? "<Null>", s, p);
        //if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IActionResult>(key);

        var selectedInfo=await FindCategoryStatusAsync(category);

        if (selectedInfo.MainCategory == null) return NotFound();

        var filterBuilder = new FilterBuilder().AddFilter(new IsApprove(true));
        if (category!=null) filterBuilder.AddFilter(new CategoryFilter(category));
        if (c.Count>0) filterBuilder.AddFilter(new SubCategoryFilter(c));
        if (b.Count > 0) filterBuilder.AddFilter(new BrandFilter(b));
        if (prc!=null) filterBuilder.AddFilter(new PriceFilter(prc));
        var filter=filterBuilder.Build();

        var pageSize = Convert.ToInt32(_configuration["PageSetting:PageSize"]);

        

        var product =await this._productService.WherePage(selectedInfo.MainCategory.Url,p, pageSize, s, filter);

        
        return View(new ProductInfo
        {
            PageInfo = new PageInfo
            {
                TotalItems = product.data.TotalCount,
                CurrentPage = p,
                ItemsPerPage = pageSize,
                MostPrice =product.data.MaxPrice!=null?Convert.ToInt32(product.data.MaxPrice):null,
                Url= category,
                Selected =new SelectedInfo 
                { 
                    SelectedSubCategories=c,
                    Category=category,
                    Brands=b,
                    CategoryType= selectedInfo.CategoryType,
                    SelectedMainCategory= selectedInfo.MainCategory,
                    SelectedCategory= selectedInfo.Category,
                    SelectedSubCategory= selectedInfo.SubCategory,
                    SelectedBrands= selectedInfo.Brands.GroupBy(x=>x.Name).Select(x=>x.First()).ToList(),
                    Sort=s,
                    Page=p,
                    Search=null,
                    Price=prc,
                }
            },
            Products = ObjectMapper.Mapper.Map<List<ProductModel>>(product.data.Product.ToList())
        });;
    }

    

    

    //[CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 60)]
    public async Task<IActionResult> Search(string? q, List<string> c, List<string> b, string? prc = null, int s = 1, int p = 1)
    {
        //var key = Reflection.CreateCacheKey(typeof(ProductController), "Search", q, c, prc ?? "<Null>", s, p);
        //if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IActionResult>(key);

        if (q == null) return RedirectToAction("products");

        var filterBuilder = new FilterBuilder().AddFilter(new IsApprove(true)).AddFilter(new SearchFilter(q));
        if (c.Count > 0) filterBuilder.AddFilter(new SubCategoryFilter(c));
        if (b.Count > 0) filterBuilder.AddFilter(new BrandFilter(b));
        if (prc != null) filterBuilder.AddFilter(new PriceFilter(prc));
        var filter = filterBuilder.Build();

        var pageSize = Convert.ToInt32(_configuration["PageSetting:PageSize"]);
        var product = await this._productService.WherePage(null, p, pageSize, s, filter);


        var categoriesss =await GetCategoriesAsync();
        var brands=categoriesss.SelectMany(x => x.Categories.SelectMany(x => x.SubCategories.SelectMany(x => x.Brands)));
        return View(new ProductInfo
        {
            PageInfo = new PageInfo
            {
                TotalItems = product.data.TotalCount,
                CurrentPage = p,
                ItemsPerPage = pageSize,
                MostPrice = product.data.MaxPrice != null ? Convert.ToInt32(product.data.MaxPrice) : null,
                Url = "/search",
                Selected = new SelectedInfo
                {
                    SelectedSubCategories = c,
                    Category = null,
                    Brands = b,
                    CategoryType = 2,
                    SelectedMainCategory = null,
                    SelectedCategory = new CategoryModel
                    {
                        SubCategories=categoriesss.SelectMany(x=>x.Categories.SelectMany(x=>x.SubCategories)).GroupBy(x => x.Name).Select(x => x.First()).ToList(),
                    },
                    SelectedSubCategory = null,
                    SelectedBrands = brands.GroupBy(x => x.Name).Select(x => x.First()).ToList(),
                    Sort = s,
                    Page = p,
                    Search = q,
                    Price = prc,
                }
            },
            Products = ObjectMapper.Mapper.Map<List<ProductModel>>(product.data.Product.ToList())
        });


    }
    //[CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 60)]
    public async Task<IActionResult> Products(List<string> c, List<string> b, string? prc = null, int s = 1, int p = 1)
        {
            //var key = Reflection.CreateCacheKey(typeof(ProductController), "Search", q, c, prc ?? "<Null>", s, p);
            //if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IActionResult>(key);


            var filterBuilder = new FilterBuilder().AddFilter(new IsApprove(true));
            if (c.Count > 0) filterBuilder.AddFilter(new SubCategoryFilter(c));
            if (b.Count > 0) filterBuilder.AddFilter(new BrandFilter(b));
            if (prc != null) filterBuilder.AddFilter(new PriceFilter(prc));
            var filter = filterBuilder.Build();

            var pageSize = Convert.ToInt32(_configuration["PageSetting:PageSize"]);
            var product = await this._productService.WherePage(null, p, pageSize, s, filter);


            var categoriesss = await GetCategoriesAsync();
            var brands = categoriesss.SelectMany(x => x.Categories.SelectMany(x => x.SubCategories.SelectMany(x => x.Brands)));
            return View(new ProductInfo
            {
                PageInfo = new PageInfo
                {
                    TotalItems = product.data.TotalCount,
                    CurrentPage = p,
                    ItemsPerPage = pageSize,
                    MostPrice = product.data.MaxPrice != null ? Convert.ToInt32(product.data.MaxPrice) : null,
                    Url = "/products",
                    Selected = new SelectedInfo
                    {
                        SelectedSubCategories = c,
                        Category = null,
                        Brands = b,
                        CategoryType = 2,
                        SelectedMainCategory = null,
                        SelectedCategory = new CategoryModel
                        {
                            SubCategories = categoriesss.SelectMany(x => x.Categories.SelectMany(x => x.SubCategories)).GroupBy(x => x.Name).Select(x => x.First()).ToList(),
                        },
                        SelectedSubCategory = null,
                        SelectedBrands = brands.GroupBy(x => x.Name).Select(x => x.First()).ToList(),
                        Sort = s,
                        Page = p,
                        Search = null,
                        Price = prc,
                    }
                },
                Products = ObjectMapper.Mapper.Map<List<ProductModel>>(product.data.Product.ToList())
            });
    }
}
