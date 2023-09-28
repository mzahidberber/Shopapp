using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.web.Extensions;
using shopapp.web.Helpers;
using shopapp.web.Mapper;
using shopapp.web.Models.Cart;
using shopapp.web.Models.Checkout;
using shopapp.web.Models.Entity;
using System.Linq;

namespace shopapp.web.Controllers;

[LogAspectController]
//[AutoValidateAntiforgeryToken]
[Authorize]
public class CheckoutController : Controller
{
    public IOrderService _orderService { get; set; }
    public UserManager<User> _userManager { get; set; }
    public IProductService _productService { get; set; }
	public CheckoutController(IOrderService orderService, UserManager<User> userManager, IProductService productService)
	{
		_orderService = orderService;
		_userManager = userManager;
		_productService = productService;
	}
	public IActionResult Index()
    {
        var cart = HttpContext.Session.Get<CartModel>("Cart");
        if(cart == null || cart.CartItems.Count == 0)
        {
            TempDataMessage.CreateMessage(TempData, "message", message: "Cart is empty!");
            return RedirectToAction("Index", "Cart");
        }
        return View(new CheckOutModel()
        {
            Products=cart.CartItems.Select(x=>new CheckoutProduct
            {
                Id=x.Product.Id,
                Name=x.Product.Name,
                ImageUrl=x.Product.HomeImageUrl,
                Price=x.Product.Price,
                Quantity=x.Quantity

            }).ToList()
        });
    }

	[HttpPost]
	public async Task<IActionResult> Index(CheckOutModel model)
	{
		if (!ModelState.IsValid)
            return View(model);
		HttpContext.Session.Set<CartModel>("Cart", new CartModel());
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if(user == null)
        {
			TempDataMessage.CreateMessage(TempData, "message", message: "User not found!");
			return View(model);
		}
        var newstocks = model.Products.Select(x => new ProductStockChange
        {
            productId = x.Id,
            quantity = x.Quantity
        }).ToList();
		var result = await _productService.ReductionManyProductStock(newstocks);

        if (result.statusCode != 200)
        {
			TempDataMessage.CreateMessage(TempData, "message", message: string.Join(",", result.error.Errors.ToArray()));
            return View(model);
		}


		var orderItems = model.Products.Select(x => new OrderItemModel
        {
            ProductId=x.Id,
            Price=x.Price,
            Quantity=x.Quantity

        }).ToList();
		var order = new OrderModel()
        {
            OrderNumber = Guid.NewGuid().ToString(),
            State = Models.Entity.EnumOrderState.HasbeenTaken,
            OrderTime = DateTime.Now,
            TotalPrice = model.TotalPrice(),
            UserId=user.Id,
            OrderItems= orderItems,
            FirstName=model.FirstName,
            LastName=model.LastName,
            Address=model.Adress,
            Email=model.Email,
            Phone=model.Phone,
            City=model.City,
            CityState=model.CityState,
            Note=model.Note,
            PaymentId = Guid.NewGuid().ToString(),
			ConversationId= Guid.NewGuid().ToString(),
			PaymentType= Models.Entity.EnumPaymentType.CreditCard

		};
		
		await _orderService.AddAsync(ObjectMapper.Mapper.Map<OrderDTO>(order));


		return View("Success");
	}
}
