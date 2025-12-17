using EShop.Application.Interfaces;
using EShop.Domain.Dtos.UserAgg;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace EShop.Web.Pages.Account
{
    public class RegisterModel(IUserService userService, SignInManager<IdentityUser<int>> signInManager
        , UserManager<IdentityUser<int>> userManager,RoleManager<IdentityRole<int>> roleManager) : PageModel
    {
        [BindProperty]
        public RegisterUserDto model { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new IdentityUser<int>
            {
                UserName = model.UserName,
                PhoneNumber = model.UserName,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);
          

            if (result.Succeeded)
            {
                model.IdentityUserId = user.Id;
                userService.Register(model);
               // await userManager.AddToRoleAsync(user,"User");
                await userManager.AddToRoleAsync(user, model.Role);
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
