using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.core.Business.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.web.EmailService;
using shopapp.web.Extensions;
using shopapp.web.Models;

namespace shopapp.web.Controllers
{
    [AutoValidateAntiforgeryToken]
	//[Authorize]
	public class AccountController:Controller
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
		//[AllowAnonymous]
		public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

		public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user=await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı ile daha önce hesap oluşturulmamıs.");
                return View(model);
            }

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen Hesabınızı onaylayınız!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/");
            }
            ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");
            return View(model);
        }
        [HttpGet]

		public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };

            var result=await _userManager.CreateAsync(user,model.Password);
            if(result.Succeeded)
            {
                //generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId=user.Id,
                    token=code
                });
                //Console.WriteLine(url);
                //email
                await this._emailSender.SendEmailAsync(user.Email,"Hesabınızı onaylayınız.",$"Lütfen Hesabınızı onaylamak için linke <a href='http://localhost:5119{url}'>tıklayınız</a>");

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("","Bilinmeyen bir hata oldu!");
                
            return View(model);
        }

		public async Task<IActionResult> Logout()
        {
			
			await _signInManager.SignOutAsync();
			TempData.Put("message", new AlertMessage()
			{
				Title = "Çıkış yapıldı",
				Message = "Çıkış yapıldı",
				AlertType = "success"
			});
			return Redirect("~/");
        
        }

		public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId==null || token == null)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title="Geçersiz Token",
                    Message="Geçersiz Token",
                    AlertType="danger"
                });
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    //cart oluştur
                    await _cartService.AddAsync(new CartDTO(){ UserId = userId });
					TempData.Put("message", new AlertMessage()
					{
						Title = "Hesabını onaylandı",
						Message = "Hesabını onaylandı",
						AlertType = "success"
					});
					return View();
                }
                
            }
			TempData.Put("message", new AlertMessage()
			{
				Title = "Hesabını onaylanmadı",
				Message = "Hesabını onaylanmadı",
				AlertType = "warning"
			});
			return View();

        }

		public async  Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> ForgetPassword(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return View();
            }
            var user=await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View();
            }

            // generate token
            var code=await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            //email
            await this._emailSender.SendEmailAsync(email, "Reset Password", $"Parolanızı yenilemek için linke <a href='http://localhost:5119{url}'>tıklayınız</a>");

            return RedirectToAction("Login", "Account");
        }

		public async Task<IActionResult> ResetPassword(string userId,string token)
        {
            if(userId==null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ResetPasswordModel { Token = token };
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var result=await _userManager.ResetPasswordAsync(user,model.Token ,model.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

		public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
