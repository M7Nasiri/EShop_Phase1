using AutoMapper;
using EShop.Application.Interfaces;
using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Entities;
using EShop.Web.ViewModels.UserAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EShop.Web.Pages.Admin
{
    [Authorize]
    public class UserManagementModel(IUserService _userService, IMapper _mapper) : PageModel
    {
        public List<UserInfoForAdminDto> Users { get; set; }
        public void OnGet()
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Users = _mapper.Map<List<UserInfoForAdminDto>>(_userService.GetAll());
           // Users = _mapper.Map<List<UserInfoForAdminDto>>(_userService.GetAllNotCurrent(userId));
        }
    }
}
