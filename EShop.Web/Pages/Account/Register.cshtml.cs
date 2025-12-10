using EShop.Application.Interfaces;
using EShop.Domain.Dtos.UserAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace EShop.Web.Pages.Account
{
    public class RegisterModel(IUserService userService) : PageModel
    {
        [BindProperty]
        public RegisterUserDto model { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (userService.Register(model))
            {
                Log.Information("User registered");
                return RedirectToPage("/Account/Login");
            }
            else
            {
                ModelState.AddModelError("model.UserName", "نام کاربری قبلا انتخاب شده است.");
                return Page();
            }
        }
    }
}
