using EShop.Application.Interfaces;
using EShop.Domain.Dtos.UserAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EShop.Web.Pages.User
{
    public class DetailsModel(IUserService _userService) : PageModel
    {
        public GetUserOrdersDto UserDetails { get; set; }
        public void OnGet(int id)
        {
            UserDetails = _userService.GetUserOrders(id);
        }


    }
}
