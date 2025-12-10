using AutoMapper;
using EShop.Application.Dtos;
using EShop.Application.Interfaces;
using EShop.Application.Services;
using EShop.Domain.Dtos.ProductAgg;
using EShop.Domain.Entities;
using EShop.Web.ViewModels.ProductAgg;
using Infra.Data.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.Security.Claims;

namespace EShop.Web.Pages
{
    public class IndexModel(IProductService _productService,IMapper mapper,ICartService _cartService
        ,ICategoryService _categoryService) : PageModel
    {

        [BindProperty]
        public List<ShowProductDto> Products { get; set; }
        [BindProperty]
        public List<CartItemDto> CartItems { get; set; }

        [BindProperty]
        public List<Category> Categories { get; set; }

        [BindProperty]
        public GroupingByCategoryDto Group { get; set; }
        [BindProperty]
        public int UserId { get; set; }

        public async Task OnGet()
        {
            Products = _productService.GetAllProductsForShow();
            Categories = _categoryService.GetAll();
            Log.Information("Information : Load Index Page");
            Log.Error("Error : Load Index Page");
            Log.Warning("Warning : Load Index Page");
            if (User.Identity.IsAuthenticated)
            {
                UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                CartItems =  mapper.Map<List<CartItemDto>>(await _cartService.GetUserCartItems(UserId));
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

        public async Task<IActionResult> OnPostGroupingBy()
        {
            Products = _productService.GroupingByCategory(Group);
            Categories = _categoryService.GetAll();
            if (User.Identity.IsAuthenticated)
            {
                UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                CartItems = mapper.Map<List<CartItemDto>>(await _cartService.GetUserCartItems(UserId));
            }
            return Page();
            //return RedirectToPage(new {id = userId });
        }

     
    }

    
}
