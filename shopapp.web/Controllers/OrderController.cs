using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.core.Business.Abstract;
using shopapp.core.Entity.Concrete;
using shopapp.web.Helpers;
using shopapp.web.Mapper;
using shopapp.web.Models.Entity;
using shopapp.web.Models.Order;
using System.Data;

namespace shopapp.web.Controllers;
[Authorize]
public class OrderController : Controller
{
	private SignInManager<User> _signInManager { get; set; }
	public UserManager<User> _userManager { get; set; }
    public IOrderService _orderService { get; set; }
	public OrderController(UserManager<User> userManager, IOrderService orderService, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_orderService = orderService;
		_signInManager = signInManager;
	}
	public async Task<IActionResult> Index(string UserName)
    {
		if (UserName == null || User.Identity.Name!=UserName)
		{
			TempDataMessage.CreateMessage(TempData, "message", message: "User not found or not authority");
			return View(new List<OrderModel>());
		}
		var user = await _userManager.FindByNameAsync(UserName);
		if (user == null)
		{
			TempDataMessage.CreateMessage(TempData, "message", message: "User not found.");
			return View(new List<OrderModel>());
		}
		var orders = await _orderService.WhereWithProducts(x => x.UserId == user.Id);
		var orderssort= orders.data.ToList().OrderBy(x => x.OrderTime).Reverse().ToList();
		return View(ObjectMapper.Mapper.Map<List<OrderModel>>(orderssort));
	}
	[HttpPost]
	public async Task<IActionResult> CancelOrder(string userName)
	{
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> ReturnOrderSingle(string userName)
	{
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> ReturnOrderPlural(string userName)
	{
		return View();
	}
}
