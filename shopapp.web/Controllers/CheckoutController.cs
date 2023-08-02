using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shopapp.core.Aspects.Logging;

namespace shopapp.web.Controllers
{
    [LogAspectController]
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
