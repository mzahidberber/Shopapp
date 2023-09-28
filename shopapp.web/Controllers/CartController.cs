using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.Entity.Concrete;
using shopapp.web.Extensions;
using shopapp.web.Helpers;
using shopapp.web.Mapper;
using shopapp.web.Models.Cart;
using shopapp.web.Models.Entity;

namespace shopapp.web.Controllers
{
    //[Authorize]
    [LogAspectController]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private UserManager<User> _userManager;
        private IProductService _productService;
        public CartController(ICartService cartService, UserManager<User> userManager, IProductService productService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var cart = HttpContext.Session.Get<CartModel>("Cart");
            return View(cart ?? new CartModel());
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity,string? returnUrl)
        {
            var cart = HttpContext.Session.Get<CartModel>("Cart");
            var product = await _productService.GetByIdAsync(productId);
            if (product.data.Stock < quantity)
            {
				TempDataMessage.CreateMessage(TempData, "message", message: $"{product.data.Stock} {product.data.Name} left");
				return Redirect(returnUrl);
			}
			if (cart == null)
            {
                var newCart = new CartModel()
                {
                    CartItems = new List<CartItemModel>()
                    {
                        new CartItemModel() {
                            Product=ObjectMapper.Mapper.Map<ProductModel>(product.data),
                            ProductId = productId,
                            Quantity = quantity}
                    }
                };
                HttpContext.Session.Set<CartModel>("Cart", newCart);
            }
            else
            {
                var index = cart.CartItems.FindIndex(x => x.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItemModel
                    {
                        Product = ObjectMapper.Mapper.Map<ProductModel>(product.data),
                        ProductId = productId,
                        Quantity = quantity
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }
                HttpContext.Session.Set<CartModel>("Cart", cart);
            }
			if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}

			return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ChangeProductQuantity(int quantity,int productId)
        {
            var cart = HttpContext.Session.Get<CartModel>("Cart");
            var product= cart.CartItems.Find(x => x.ProductId == productId);
            product.Quantity = quantity;
            HttpContext.Session.Set<CartModel>("Cart", cart);
            return Json(new
            {
                ProductTotalPrice=product.Product.Price*quantity,
                TotalPrice = cart.TotalPrice()
            });
        }

        [HttpPost]
        public IActionResult DeleteToCart(int productId)
        {
            var cart = HttpContext.Session.Get<CartModel>("Cart");
            cart.CartItems.Remove(cart.CartItems.First(x => x.ProductId == productId));
            HttpContext.Session.Set<CartModel>("Cart", cart);
            return RedirectToAction("Index");
        }
    }
}
