using EShop.Application.Interfaces;
using EShop.Application.Services;
using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Entities;
using EShop.Domain.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.Security.Claims;
using System.Text.Json;
namespace EShop.Web.Pages.Account
{
    public class LoginModel(IUserService userService,ICartService cartService) : PageModel
    {
        [BindProperty]
        public LoginUserDto model { get; set; }

        [Route("Login")]
        public void OnGet(string ReturnUrl = "/index")
        {
            ViewData["ReturnUrl"] = ReturnUrl;
        }

        [Route("Login")]
        public async Task<IActionResult> OnPost(string ReturnUrl = "/index")
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = userService.Login(model);
            if (result == null)
            {
                ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
                return Page();
            }
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
                //userService.UpdateRememberMe(model.Id, model.RememberMe);


                var guestCartCookie = Request.Cookies["guest_cart"];
                if (!string.IsNullOrEmpty(guestCartCookie))
                {
                    try
                    {
                        var guestItems = JsonSerializer.Deserialize<List<GuestCartItem>>(guestCartCookie);
                        if (guestItems != null && guestItems.Count > 0)
                        {
                            await cartService.MergeGuestCartIntoUserCart(result.Id, guestItems);
                        }
                    }
                    catch
                    {
                        // اگر JSON خراب بود، نادیده می‌گیریم
                    }

                   // Response.Cookies.Delete("guest_cart");
                }


                if (result.Role == RoleEnum.NormalUser)
                {
                    Log.Information("User Logged in");
                    return RedirectToPage("/Index");
                }
                if (result.Role == RoleEnum.Admin)
                {
                    Log.Information("Admin Logged in");
                    return RedirectToPage("/Admin/ProductManagement");
                }
            }
            //ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
            return Page();
        }
    }
}
