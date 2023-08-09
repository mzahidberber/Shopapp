using Microsoft.AspNetCore.Mvc;
using shopapp.web.Models.Shared;

namespace shopapp.web.ViewComponents
{
    public class SortViewComponent: ViewComponent
    {
        public List<Sort> SortTypes { get; set; } = new List<Sort>{
            new Sort{Name="Suggested",Value=1},
            new Sort{Name="Descending by Price",Value=2},
            new Sort { Name = "Increasing by Price", Value = 3 }};
        public IViewComponentResult Invoke()
        {
            return View(SortTypes);
        }
    }
}
