using AutoMapper;
using EShop.Application.Dtos;
using EShop.Application.Interfaces;
using EShop.Application.Services;
using EShop.Domain.Entities;
using EShop.Domain.ViewModels.ProductAgg;
using Infra.Data.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EShop.Web.Pages
{
    public class IndexModel(AppDbContext context,IMapper mapper,ICartService _cartService) : PageModel
    {
        [BindProperty]
        public List<ShowProductViewModel> Products { get; set; }
        [BindProperty]
        public List<CartItemDto> CartItems { get; set; }
        public async Task OnGet()
        {
            Products = mapper.Map<List<ShowProductViewModel>>(context.Products.ToList());
            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                CartItems =  mapper.Map<List<CartItemDto>>(await _cartService.GetUserCartItems(userId));
            }
            else
            {
                CartItems = new List<CartItemDto>();
            }
        }
        //public async Task<IActionResult> OnPostAddToCart([FromBody] AddToCartDto dto)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return new JsonResult(new { isGuest = true });
        //    }

        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    await _cartService.AddToUserCart(userId, dto.ProductId);

        //    int count = await _cartService.GetUserCartCount(userId);

        //    return new JsonResult(new { isGuest = false, count });
        //}

       

        public class AddToCartDto
        {
            public int ProductId { get; set; }
        }

     
    }

    
}
