using Microsoft.AspNetCore.Mvc;
using shopapp.core.Business.Abstract;
using shopapp.web.Mapper;
using shopapp.web.Models;
using shopapp.web.Models.Entity;
using shopapp.web.ViewModels;
using System.Diagnostics;

namespace shopapp.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;
        private ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products=await _productService.Where(x=>x.IsApprove==true && x.IsHome==true);
            var categories=await _categoryService.GetAllAsync();
            return View(new ProductAndCategories
            {
                Products= ObjectMapper.Mapper.Map<List<ProductModel>>(products.data),
                Categories=ObjectMapper.Mapper.Map<List<CategoryModel>>(categories.data)
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}