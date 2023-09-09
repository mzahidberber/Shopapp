using Microsoft.AspNetCore.Mvc;
using shopapp.core.Aspects.Caching;
using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.CrossCuttingConcers.Caching;
using shopapp.core.CrossCuttingConcers.Caching.Microsoft;
using shopapp.core.Reflection;
using shopapp.web.Mapper;
using shopapp.web.Models;
using shopapp.web.Models.Entity;
using shopapp.web.ViewModels;
using System.Diagnostics;

namespace shopapp.web.Controllers
{
    [LogAspectController]
    public class HomeController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private ICacheManager _cacheManager;

        public HomeController(IProductService productService, ICategoryService categoryService, ICacheManager cacheManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cacheManager = cacheManager;
        }

        //[CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 1440)]
        public async Task<IActionResult> Index()
        {
            //var key = Reflection.CreateCacheKey(typeof(HomeController), "Index");
            //if (_cacheManager.IsAdd(key))
            //    return _cacheManager.Get<IActionResult>(key);


            var products = await _productService.WhereWithAtt(x => x.IsApprove == true && x.IsHome == true);
            //var categories = await _categoryService.GetAllAsync();
            return View(new ProductInfo
            {
                Products = ObjectMapper.Mapper.Map<List<ProductModel>>(products.data)
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}