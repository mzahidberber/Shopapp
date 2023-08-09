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
using shopapp.web.Models.Shared;
using shopapp.web.ViewModels;

namespace shopapp.web.Controllers
{
    [LogAspectController]
    public class ProductController : Controller
    {
        private IProductService _productService { get; set; }
        private ICategoryService _categoryService { get; set; }
        private IConfiguration _configuration { get; set; }
        public ICacheManager _cacheManager { get; set; }
        public ProductController(IProductService productService, ICategoryService categoryService, IConfiguration configuration, ICacheManager cacheManager)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._configuration = configuration;
            this._cacheManager = cacheManager;
        }

        [CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 1440)]
        public async Task<IActionResult> Index()
        {
            var key = Reflection.CreateCacheKey(typeof(ProductController), "Index");
            if (_cacheManager.IsAdd(key))
                return _cacheManager.Get<IActionResult>(key);


            var products = await this._productService.GetAllAsync();
            var categories = await this._categoryService.GetAllAsync();
            return View(new ProductList
            {
                Categories = ObjectMapper.Mapper.Map<List<CategoryModel>>(categories.data.ToList()),
                Products = ObjectMapper.Mapper.Map<List<ProductModel>>(products.data.ToList()),
            });
        }

        [CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 1440)]
        public async Task<IActionResult> Details(int id)
        {
            var key = Reflection.CreateCacheKey(typeof(ProductController), "Details", id);
            if (_cacheManager.IsAdd(key))
                return _cacheManager.Get<IActionResult>(key);



            var product = await this._productService.GetByIdWithCategoriesAsync(id);
            return View(ObjectMapper.Mapper.Map<ProductModel>(product.data));
        }


        //[CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 1440)]
        public async Task<IActionResult> List(string[] c,string? prc=null,int s=1, int p = 1)
        {
            //var key = Reflection.CreateCacheKey(typeof(ProductController), "List", c, prc??"<Null>",s,p);
            //if (_cacheManager.IsAdd(key))
            //    return _cacheManager.Get<IActionResult>(key);

            ViewBag.Sort = s;
            ViewBag.Page = p;
            ViewBag.SelectedCategories = c;
            ViewBag.SelectedPrice=prc;
            ViewBag.Url = "/product";

            var pageSize = Convert.ToInt32(_configuration["PageSetting:PageSize"]);

            ProductDTOAndTotalCount product;

            if (c.Length > 0)
            {
                if (prc != null)
                {
                    var prcList = prc.Split("-");
                    if (prcList[1] != "")
                    {
                        product = this._productService.WherePage(p, pageSize, s, x =>
                            x.Price <= Convert.ToDouble(prcList[1]) &&
                            x.Price >= Convert.ToDouble(prcList[0]) &&
                            x.ProductCategories.Any(p => c.Any(x => x == p.Category.Url))
                        ).Result.data;
                    }else
                    {
                        product = this._productService.WherePage(p, pageSize, s, x =>
                            x.Price >= Convert.ToDouble(prcList[0]) &&
                            x.ProductCategories.Any(p => c.Any(x => x == p.Category.Url))
                        ).Result.data;
                    }
                    
                }
                else
                {

                    product = this._productService.WherePage(p, pageSize, s, x =>
                        x.ProductCategories.Any(p => c.Any(x => x == p.Category.Url))
                    ).Result.data;
                }
            }
            else
            {
                if (prc != null)
                {
                    var prcList = prc.Split("-");
                    if (prcList[1] !="")
                    {
                        product = this._productService.WherePage(p, pageSize, s, x =>
                            x.Price <= Convert.ToDouble(prcList[1]) &&
                            x.Price >= Convert.ToDouble(prcList[0])
                        ).Result.data;
                    }
                    else
                    {
                        product = this._productService.WherePage(p, pageSize, s, x =>
                            x.Price >= Convert.ToDouble(prcList[0])
                        ).Result.data;
                    }
                }
                else
                {

                    product = this._productService.WherePage(p, pageSize, s).Result.data;
                }
            }


            return View(new ProductList
            {
                PageInfo = new PageInfo
                {
                    TotalItems = product.TotalCount,
                    CurrentPage = p,
                    ItemsPerPage = pageSize
                },
                MostPrice = Convert.ToInt32(product.MaxPrice),
                Products = ObjectMapper.Mapper.Map<List<ProductModel>>(product.Product.ToList())
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await this._categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories.data, "Id", "Name");
            return View(new ProductModel());
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //isime bak
                    var extension = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extension}");
                    product.HomeImageUrl = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                await this._productService.AddAsync(ObjectMapper.Mapper.Map<ProductDTO>(product));
                return RedirectToAction("list");
            }
            return View(product);
        }

        [CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 1440)]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var key = Reflection.CreateCacheKey(typeof(ProductController), "Edit", id);
            if (_cacheManager.IsAdd(key))
                return _cacheManager.Get<IActionResult>(key);


            var categories = await this._categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories.data.ToList(), "Id", "Name");
            var product = this._productService.GetByIdWithCategoriesAsync(id).Result.data;
            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            Console.WriteLine(product.ProductCategories.Count());
            foreach (var item in product.ProductCategories)
            {
                Console.WriteLine(item.ProductId);
                Console.WriteLine(item.CategoryId);
            }
            await this._productService.Update(product, product.Id);
            return RedirectToAction("list");
        }

        [CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 1440)]
        public async Task<IActionResult> Search(string q)
        {
            var key = Reflection.CreateCacheKey(typeof(ProductController), "Search", q);
            if (_cacheManager.IsAdd(key))
                return _cacheManager.Get<IActionResult>(key);


            if (RouteData.Values["action"].ToString() == "Search")
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            if (q == null)
                q = "";
            var product = await _productService.Where(x => x.Description.ToLower().Contains(q.ToLower()) || x.Name.ToLower().Contains(q.ToLower()));
            var categories = await this._categoryService.GetAllAsync();
            return View(new ProductList
            {
                //PageInfo = new PageInfo
                //{
                //    Url = "/product",
                //    TotalItems = product.TotalCount,
                //    CurrentPage = page,
                //    ItemsPerPage = pageSize,
                //    CurrentCategory = category
                //},
                Categories = ObjectMapper.Mapper.Map<List<CategoryModel>>(categories.data.ToList()),
                Products = ObjectMapper.Mapper.Map<List<ProductModel>>(product.data.ToList()),
            });
        }
    }
}
