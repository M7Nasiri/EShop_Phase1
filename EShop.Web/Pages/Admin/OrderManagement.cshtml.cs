using EShop.Application.Interfaces;
using EShop.Application.Services;
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

            try
            {
                await _orderService.ShippededOrder(id);
                return RedirectToPage("/Admin/OrderManagement");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return RedirectToPage("/Admin/OrderManagement", new  {Id = id });
        }
    }
}
