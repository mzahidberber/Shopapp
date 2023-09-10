using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.web.EmailService;
using shopapp.web.Helpers;
using shopapp.web.Models.Account;

namespace shopapp.web.Controllers
{
    [AutoValidateAntiforgeryToken]
    //[Authorize]
    [LogAspectController]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        private ICartService _cartService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cartService = cartService;
        }


        public IActionResult Login(string? ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                TempDataMessage.CreateMessage(TempData, "SingInMessage", message: "Username or password is incorrect.");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                TempDataMessage.CreateMessage(TempData, "SingInMessage", message: "Please confirm your account.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded) return Redirect(model.ReturnUrl ?? "~/");

            TempDataMessage.CreateMessage(TempData, "SingInMessage", message: "Username or password is incorrect.");
            return View(model);
        }
        [HttpGet]
        public IActionResult Register() => View(new RegisterModel());


        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                //email
                //url degiştir
                await this._emailSender.SendEmailAsync(user.Email, "ShopApp - Confirm your account.", $"Please confirm your account.<a href='http://localhost:5119{url}'>Link</a>");

                TempDataMessage.CreateMessage(TempData, key: "message", message: "User created.Please confirm your account.");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempDataMessage.CreateMessage(TempData,
                    key: "message", message: string.Join(",", result.Errors.Select(x => x.Description)),
                    alertType: "warning");
            }


            //TempDataMessage.CreateMessage(TempData, key: "SingInMessage", message: "An unknown error has occurred.Please try again.");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempDataMessage.CreateMessage(TempData, key: "message", message: "Logged out.");
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempDataMessage.CreateMessage(TempData, "message", message: "Token invalid", alertType: "danger");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    //cart oluştur
                    await _cartService.AddAsync(new CartDTO() { UserId = userId });
                    TempDataMessage.CreateMessage(TempData, key: "message", message: "Your account has been confirmed.");
                    return View();
                }

            }
            TempDataMessage.CreateMessage(TempData, "message", message: "Your account has been not confirmed.");
            return View();

        }

        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempDataMessage.CreateMessage(TempData, "SingInMessage", message: "Email required.");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempDataMessage.CreateMessage(TempData, "SingInMessage", message: "Email not found.");
                return View();
            }

            // generate token
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            //email
            //Urli degiştir
            await this._emailSender.SendEmailAsync(email, "Reset Password", $"Click on the link to reset your password.<a href='http://localhost:5119{url}'>Link</a>");

            return RedirectToAction("Login", "Account");
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempDataMessage.CreateMessage(TempData, "SingInMessage", message: "User not found.");
                return View();
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
                return RedirectToAction("Login", "Account");
            else
                TempDataMessage.CreateMessage(TempData, "SingInMessage", message: string.Join("\n- ", result.Errors.Select(x => x.Description)));

            return View(model);
        }

        public IActionResult AccessDenied() => View();
    }
}
