﻿using Microsoft.AspNetCore.Mvc;
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
using shopapp.web.ProductFilter;
using shopapp.web.ViewModels;
using System.Reflection.Metadata;
using System.Reflection;

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


        //[CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 60)]
        public async Task<IActionResult> Details(int id)
        {
            //var key = Reflection.CreateCacheKey(typeof(ProductController), "Details", id);
            //if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IActionResult>(key);



            var product = await this._productService.GetByIdWithCategoriesAndImagesAsync(id);
            return View(ObjectMapper.Mapper.Map<ProductModel>(product.data));
        }


        //[CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 60)]
        public async Task<IActionResult> Index(string[] c,string? prc=null,int s=1, int p = 1)
        {
            //var key = Reflection.CreateCacheKey(typeof(ProductController), "List",c, prc ?? "<Null>", s, p);
            //if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IActionResult>(key);


            var filterBuilder = new FilterBuilder();
            if (c.Length>0) filterBuilder.AddFilter(new CategoryFilter(c));
            if (prc!=null) filterBuilder.AddFilter(new PriceFilter(prc));
            var filter=filterBuilder.Build();

            var pageSize = Convert.ToInt32(_configuration["PageSetting:PageSize"]);
            var product =await this._productService.WherePage(p, pageSize, s, filter);

            
            return View(new ProductInfo
            {
                PageInfo = new PageInfo
                {
                    TotalItems = product.data.TotalCount,
                    CurrentPage = p,
                    ItemsPerPage = pageSize,
                    MostPrice = Convert.ToInt32(product.data.MaxPrice),
                    Url= "/product",
                    Selected =new SelectedInfo 
                    { 
                        Categories=c,
                        Sort=s,
                        Page=p,
                        Search=null,
                        Price=prc,
                    }
                },
                Products = ObjectMapper.Mapper.Map<List<ProductModel>>(product.data.Product.ToList())
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
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 60)]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var key = Reflection.CreateCacheKey(typeof(ProductController), "Edit", id);
            if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IActionResult>(key);


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
            return RedirectToAction("Index");
        }

        //[CacheAspectController(typeof(MemoryCacheManager), cacheByMinute: 60)]
        public async Task<IActionResult> Search(string? q, string[] c, string? prc = null, int s = 1, int p = 1)
        {
            //var key = Reflection.CreateCacheKey(typeof(ProductController), "Search", q, c, prc ?? "<Null>", s, p);
            //if (_cacheManager.IsAdd(key)) return _cacheManager.Get<IActionResult>(key);

            if (q == null) return RedirectToAction("Index");

            var filterBuilder = new FilterBuilder();
            if (c.Length > 0) filterBuilder.AddFilter(new CategoryFilter(c));
            if (prc != null) filterBuilder.AddFilter(new PriceFilter(prc));
            filterBuilder.AddFilter(new SearchFilter(q));
            var filter = filterBuilder.Build();

            var pageSize = Convert.ToInt32(_configuration["PageSetting:PageSize"]);
            var product = await this._productService.WherePage(p, pageSize, s, filter);


            return View(new ProductInfo
            {
                PageInfo = new PageInfo
                {
                    TotalItems = product.data.TotalCount,
                    CurrentPage = p,
                    ItemsPerPage = pageSize,
                    MostPrice = Convert.ToInt32(product.data.MaxPrice),
                    Url = "/search",
                    Selected = new SelectedInfo
                    {
                        Categories = c,
                        Sort = s,
                        Page = p,
                        Search = q,
                        Price = prc,
                    }
                },
                Products = ObjectMapper.Mapper.Map<List<ProductModel>>(product.data.Product.ToList())
            });
        }
    }
}
