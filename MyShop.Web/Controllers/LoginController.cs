using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyShop.Web.ViewModels;
using MyShop.Infrastructure.Customer;
using System.Threading.Tasks;

public class AuthController : Controller
{
    private readonly SignInManager<CustomerIdentity> _signInManager;
    private readonly UserManager<CustomerIdentity> _userManager;

    public AuthController(SignInManager<CustomerIdentity> signInManager, UserManager<CustomerIdentity> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    // GET /auth/login
    [HttpGet("auth/login")]
    public IActionResult Login()
    {
        return View();
    }

    // POST /auth/login
    [HttpPost("auth/login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Benutzer existiert nicht.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Login fehlgeschlagen.");
        return View(model);
    }

    // POST /auth/logout
    [HttpPost("auth/logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}