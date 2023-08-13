using Microsoft.AspNetCore.Mvc;
using shopapp.core.Business.Abstract;
using shopapp.web.Mapper;
using shopapp.web.Models.Entity;

namespace shopapp.web.ViewComponents;

public class NavMainCategoryViewComponent: ViewComponent
{
    public IMainCategoryService _mainCategoryService { get; set; }
	public NavMainCategoryViewComponent(IMainCategoryService mainCategoryService)
	{
		_mainCategoryService = mainCategoryService;
	}
	public IViewComponentResult Invoke()
    {
        var mainCategories = this._mainCategoryService.GetAllWithCategoriAndSubCategories().Result.data.ToList();

        return View(ObjectMapper.Mapper.Map<List<MainCategoryModel>>(mainCategories));
    }
}
