using EShop.Application.Interfaces;
using EShop.Domain.Enum;
using EShop.Domain.ViewModels.UserAgg;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
namespace EShop.Web.Pages.Account
{
    public class LoginModel(IUserService userService) : PageModel
    {
        [BindProperty]
        public LoginUserViewModel model { get; set; }

        [Route("Login")]
        public void OnGet(string ReturnUrl = "/index")
        {
            ViewData["ReturnUrl"] = ReturnUrl;
        }

        [Route("Login")]
        public IActionResult OnPost(string ReturnUrl = "/index")
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = userService.Login(model);
            if (result != null)
            {
                model.Role = result.Role;
                var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name,model.UserName),
                new Claim(ClaimTypes.NameIdentifier,result.Id.ToString()),
                new Claim(ClaimTypes.Role,model.Role.ToString())
                };
                var identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = model.RememberMe
                };
                HttpContext.SignInAsync(principal, properties);
                userService.UpdateRememberMe(model.Id, model.RememberMe);
                if (result.Role == RoleEnum.NormalUser)
                {
                    return RedirectToPage("/Index");
                }
                if (result.Role == RoleEnum.Writer)
                {
                    return RedirectToPage("/Writer/Profile");
                }
                if (result.Role == RoleEnum.Verifier)
                {
                    return RedirectToPage("/Verifier/Index");
                }
                if (result.Role == RoleEnum.Admin)
                {
                    return RedirectToPage("/Admin/Index");
                }
            }
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
            return Page();
        }
    }
}
