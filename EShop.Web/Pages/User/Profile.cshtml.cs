using EShop.Application.Interfaces;
using EShop.Domain.Dtos.UserAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EShop.Web.Pages.User
{
    [Authorize(Roles = "User,Admin")]
    public class ProfileModel(UserManager<IdentityUser<int>> _userManager,IUserService _userService,IOrderService _orderService) : PageModel
    {
        [BindProperty]
        public ShowUserPrfofileDto ProfileInfo { get; set; }
        public int UserId { get; set; }
        public async Task OnGet()
        {
            UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            ProfileInfo = await _userManager.Users.Where(u => u.Id == UserId).Select(p => new ShowUserPrfofileDto
            {
                Id = UserId,
                Email = p.Email,
                UserName = p.UserName,
            }).FirstOrDefaultAsync();

            ProfileInfo.UserInfo = _userService.GetUserInfo(UserId);
            ProfileInfo.Orders = await _orderService.GetUserOrders(UserId);
        }
    }

   
}
