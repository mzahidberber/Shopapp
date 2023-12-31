﻿using Microsoft.AspNetCore.Mvc;
using shopapp.web.Models.Entity;

namespace shopapp.web.ViewComponents;

public class NavMainCategoryViewComponent : ViewComponent
{
    //   public IMainCategoryService _mainCategoryService { get; set; }
    //public NavMainCategoryViewComponent(IMainCategoryService mainCategoryService)
    //{
    //	_mainCategoryService = mainCategoryService;
    //}
    public IViewComponentResult Invoke(List<MainCategoryModel> categories)
    {
        //var mainCategories = this._mainCategoryService.GetAllWithCategoriAndSubCategories().Result.data.ToList();

        return View(categories);
    }
}
