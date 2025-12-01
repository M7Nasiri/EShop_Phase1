using EShop.Application.Dtos;
using EShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EShop.Web.Pages.Cart
{
    public class OperationModel : PageModel
    {
        private readonly ICartService _cartService;
        public OperationModel(ICartService cartService) => _cartService = cartService;

        public async Task<IActionResult> OnPostIncrement([FromBody] ProductDto dto)
        {
            if (!User.Identity.IsAuthenticated)
                return new JsonResult(new { isGuest = true });

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _cartService.AddToUserCart(userId, dto.ProductId);
            var cartItems = await _cartService.GetUserCartItems(userId);

            return new JsonResult(new
            {
                cartItems = cartItems.Select(c => new {
                    productId = c.ProductId,
                    title = c.Product?.Title,
                    unitPrice = c.Product?.UnitCost,
                    quantity = c.Count
                })
            });
        }

        public async Task<IActionResult> OnPostDecrement([FromBody] ProductDto dto)
        {
            if (!User.Identity.IsAuthenticated)
                return new JsonResult(new { isGuest = true });

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _cartService.RemoveFromUserCart(userId, dto.ProductId);
            var cartItems = await _cartService.GetUserCartItems(userId);

            return new JsonResult(new
            {
                cartItems = cartItems.Select(c => new {
                    productId = c.ProductId,
                    title = c.Product?.Title,
                    unitPrice = c.Product?.UnitCost,
                    quantity = c.Count
                })
            });
        }

        public async Task<IActionResult> OnPostDelete([FromBody] ProductDto dto)
        {
            if (!User.Identity.IsAuthenticated)
                return new JsonResult(new { isGuest = true });

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _cartService.DeleteFromUserCart(userId, dto.ProductId);
            var cartItems = await _cartService.GetUserCartItems(userId);

            return new JsonResult(new
            {
                cartItems = cartItems.Select(c => new {
                    productId = c.ProductId,
                    title = c.Product?.Title,
                    unitPrice = c.Product?.UnitCost,
                    quantity = c.Count
                })
            });
        }

        public async Task<IActionResult> OnPostClearCart()
        {
            if (!User.Identity.IsAuthenticated)
                return new JsonResult(new { isGuest = true });

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _cartService.RemoveAllItemRelatedToUser(userId);

            return new JsonResult(new { cartItems = new List<object>() });

    }

    public class ProductDto
        {
            public int ProductId { get; set; }
        }
    }
}
