using EShop.Application.Interfaces;
using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EShop.Web.Pages.User
{
    public class EditInfoModel(UserManager<IdentityUser<int>> _userManager, IUserService _userService) : PageModel
    {
        [BindProperty]
        public EditUserInfoDto model { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();
            var info = _userService.GetUserInfo(userId);

            model = new EditUserInfoDto
            {
                Email = user.Email,
            };
            model.Credit = info.Credit;
            model.FullName = info.FullName;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound();

            user.Email = model.Email;
            await _userManager.UpdateAsync(user);

            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                var result = await _userManager.ChangePasswordAsync(
                    user,
                    model.CurrentPassword,
                    model.NewPassword
                );
                string message = "";
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        message = error.Description + Environment.NewLine;

                    }
                    TempData["Error"] = message;

                    return Page();
                }

            }
            _userService.SetUserInfo(userId, model.Credit, model.FullName);

            return RedirectToPage("/User/Profile");
        }
    }
}
