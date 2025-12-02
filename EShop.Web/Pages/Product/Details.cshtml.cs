using EShop.Application.Interfaces;
using EShop.Application.Services;
using EShop.Domain.ViewModels.ProductAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EShop.Web.Pages.Product
{
    public class DetailsModel(ICartService cartService,IUserService userService,IProductService _productService) : PageModel
    {
        [BindProperty]
        public ProductDetailsViewModel Product { get; set; }
        public async Task OnGet(int id)
        {
            var userId = userService.GetCurrentUserId(User);
            Product = _productService.GetProductDetailsById(id);

        }

        public async Task<IActionResult> OnPostAddToCart(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { guest = true });
            }

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await cartService.AddToUserCart(userId, productId);

            return new JsonResult(new { guest = false });
        }

    }
}
