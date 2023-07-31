using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace shopapp.web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
