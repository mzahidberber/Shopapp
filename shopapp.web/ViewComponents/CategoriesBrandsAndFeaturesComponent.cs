using Microsoft.AspNetCore.Mvc;
using shopapp.core.Business.Abstract;
using shopapp.core.CrossCuttingConcers.Caching;

namespace shopapp.web.ViewComponents;

public class CategoriesBrandsAndFeaturesComponent : ViewComponent
{
    public IMainCategoryService _mainCategoryService { get; set; }
    public ICacheManager _cacheManager { get; set; }
    public CategoriesBrandsAndFeaturesComponent(IMainCategoryService mainCategoryService, ICacheManager cacheManager)
    {
        _mainCategoryService = mainCategoryService;
        _cacheManager = cacheManager;
    }
    public IViewComponentResult Invoke()
    {
        return View();
    }

}
