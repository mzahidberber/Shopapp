using Microsoft.AspNetCore.Mvc;
using shopapp.core.Business.Abstract;
using shopapp.web.Mapper;
using shopapp.web.Models.Entity;

namespace shopapp.web.ViewComponents
{
    public class CategoryFilterViewComponent:ViewComponent
    {
        public ICategoryService _categoryService { get; set; }
        public CategoryFilterViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            var categories= this._categoryService.GetAllAsync().Result.data.ToList();
            
            return View(ObjectMapper.Mapper.Map<List<CategoryModel>>(categories));
        }
    }
}
