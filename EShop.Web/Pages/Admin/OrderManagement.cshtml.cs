using EShop.Application.Interfaces;
using EShop.Domain.Dtos.OrderAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Admin
{
    [Authorize]
    public class OrderManagementModel(IOrderService _orderService) : PageModel
    {
        public List<GetOrderDto> Orders { get; set; } = new();
        public async Task OnGetAsync()
        {
            Orders = await _orderService.GetAll();
        }

        public async Task<IActionResult> OnGetShipped(int id)
        {
            if (await _orderService.ShippededOrder(id))
            {
                return RedirectToPage("/Admin/OrderManagement", new { Id = id });
            }
            //ModelState.AddModelError("", "عملیات پرداخت ، تکمیل نشده است .");
            return RedirectToPage();

        }
    }
}
