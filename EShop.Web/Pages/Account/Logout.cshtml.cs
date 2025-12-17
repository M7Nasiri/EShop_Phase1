using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace EShop.Web.Pages.Account
{
    public class LogoutModel(SignInManager<IdentityUser<int>> signInManager ) : PageModel
    {
        public IActionResult OnGet()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            signInManager.SignOutAsync();
            Log.Information("Logged out");
            return RedirectToPage("/Account/Login");
        }
    }
}
