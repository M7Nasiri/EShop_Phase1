using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Order
{
    public class FinalModel(IOrderService _orderService) : PageModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? OrderId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public long TotalPrice { get; set; }

        public async Task<IActionResult> OnGet(int? orderId)
        {
            if (orderId == null || orderId <= 0)
            {
                IsSuccess = false;
                Message = "شناسه سفارش معتبر نیست یا پرداخت انجام نشده است.";
                return Page();
            }

            var order = await _orderService.GetOrderById(orderId.Value);
            if (order == null)
            {
                IsSuccess = false;
                Message = "سفارشی با این شناسه یافت نشد.";
                return Page();
            }

            // موفقیت
            IsSuccess = true;
            OrderId = order.Id;
            Items = order.OrderItems;
            TotalPrice = Items.Sum(x => x.UnitPrice * x.Amount);
            Message = "پرداخت شما با موفقیت انجام شد.";

            return Page();
        }
    }
}
