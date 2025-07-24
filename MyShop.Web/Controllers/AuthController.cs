using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyShop.Web.ViewModels;
using MyShop.Infrastructure.Customer;
using System.Threading.Tasks;

namespace MyShop.Web.Controllers
{
    
    public class AuthController : Controller
    {
        private readonly SignInManager<CustomerIdentity> _signInManager;
        private readonly UserManager<CustomerIdentity> _userManager;

        public AuthController(SignInManager<CustomerIdentity> signInManager, UserManager<CustomerIdentity> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: /auth/login
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml");
        }

        // POST: /auth/login
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Login/Login.cshtml", model);

            // Benutzer existiert?
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Benutzer existiert nicht.");
                return View("~/Views/Login/Login.cshtml", model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);
            Console.WriteLine($"Login result: Succeeded={result.Succeeded}, IsLockedOut={result.IsLockedOut}, IsNotAllowed={result.IsNotAllowed}");

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Benutzer ist gesperrt.");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Login nicht erlaubt.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login fehlgeschlagen. Bitte überprüfe deine Anmeldedaten.");
            }

            return View(model);
        }

        // POST: /auth/logout
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
