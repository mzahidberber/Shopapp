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
        public IConfiguration _configuration { get; set; }
        public ProductController(IProductService productService, ICategoryService categoryService, IConfiguration configuration)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._configuration = configuration;
        }
        public async Task<IActionResult> Index() 
        {
            var products = await this._productService.GetAllAsync();
            var categories = await this._categoryService.GetAllAsync();
            return View(new ProductAndCategories { Categories = categories.data.ToList(), Products = products.data.ToList() });
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await this._productService.GetByIdWithCategoriesAsync(id);
            return View(product.data);
        }

        public async Task<IActionResult> List(string category,int page=1)
        {
            if (RouteData.Values["action"].ToString() == "List")
            {
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            }
            var product = new ProductDTOAndTotalCount();
            var pageSize = Convert.ToInt32(_configuration["PageSetting:PageSize"]);
            if (category == null) product = this._productService.WherePage(page,pageSize).Result.data;
            else product =this._productService.WherePage(page, pageSize,x => x.ProductCategories.Any(x=>x.Category.Url==category)).Result.data;
            var categories = await this._categoryService.GetAllAsync();
            return View(new ProductAndCategories { PageInfo=new PageInfo
            {
                TotalItems = product.TotalCount,
                CurrentPage=page,
                ItemsPerPage=pageSize,
                CurrentCategory=category
            },
                Categories = categories.data.ToList(), 
                Products = product.Product.ToList() });
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

        public async Task<IActionResult> Search(string q)
        {
            if (RouteData.Values["action"].ToString() == "List")
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            if (q == null)
                q = "";
            var product=await _productService.Where(x=>x.Description.ToLower().Contains(q.ToLower()) || x.Name.ToLower().Contains(q.ToLower()));
            var categories = await this._categoryService.GetAllAsync();
            return View(new ProductAndCategories { Categories = categories.data.ToList(), Products = product.data.ToList() });
        }
    }
}
