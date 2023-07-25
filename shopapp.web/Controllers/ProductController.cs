using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using shopapp.core.Business.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.web.ViewModels;

namespace shopapp.web.Controllers
{
    public class ProductController:Controller
    {
        public IProductService _productService { get; set; }
        public ICategoryService _categoryService { get; set; }
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
        }
        public async Task<IActionResult> Index() 
        {
            var products = await this._productService.GetAllAsync();
            var categories = await this._categoryService.GetAllAsync();
            return View(new ProductAndCategories { Categories = categories.data.ToList(), Products = products.data.ToList() });
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await this._productService.GetByIdAsync(id);
            return View(product.data);
        }

        public async Task<IActionResult> List(int? id)
        {
            if (RouteData.Values["action"].ToString()=="List")
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            var product = new List<ProductDTO>();
            if (id == null) product = this._productService.GetAllAsync().Result.data.ToList();
            else product = this._productService.Where(x=>x.Id==id).Result.data.ToList();
            var categories = await this._categoryService.GetAllAsync();
            return View(new ProductAndCategories { Categories = categories.data.ToList(), Products = product });
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories= await this._categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories.data,"Id","Name");
            return View(new ProductDTO());
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await this._productService.AddAsync(product);
                return RedirectToAction("list");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
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
            await this._productService.Update(product,product.Id);
            return RedirectToAction("list");
        }
    }
}
