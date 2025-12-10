using EShop.Application.Interfaces;
using EShop.Domain.Dtos.Checkout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.Security.Claims;

namespace EShop.Web.Pages.Order
{
    [Authorize(Roles = "Admin,NormalUser")]
    public class CheckoutModel(ICartService _cartService,ICheckoutService _checkoutService,IUserService _userService,
        IOrderService _orderService) : PageModel
    {
        [BindProperty]
        public Checkout Invoice { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Account/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var cart = await _cartService.GetUserCartItems(userId); 

            Invoice = _checkoutService.AddUserCartItemToCheckout(cart);

            return Page();
        }

        public class PaymentDto { public bool Confirm { get; set; } }

        public async Task<IActionResult> OnPostPay([FromBody] PaymentDto dto)
        {
            if (!User.Identity.IsAuthenticated)
                return new JsonResult(new { success = false, message = "ابتدا وارد شوید" });

            if (!dto.Confirm)
                return new JsonResult(new { success = false, message = "تأیید پرداخت انجام نشد" });

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var cart = await _cartService.GetUserCartItems(userId); 

           
            var checkRes = await _orderService.CheckOrder(userId, cart);
            if (!checkRes.IsSuccess)
            {
                Log.Information($"Checkout Page : {checkRes.Message}");
                return new JsonResult(new
                {
                    success = false,
                    message = checkRes.Message
                });
            }


            int orderId = await _orderService.Finalized(userId, cart, checkRes.Data);
            Log.Information($"Order {orderId} is finalized!");
            return new JsonResult(new { success = true, orderId });
        }
    }
}
