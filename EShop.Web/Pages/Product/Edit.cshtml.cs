using EShop.Domain.ViewModels.ProductAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Product
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public UpdateProductViewModel Product { get; set; }
        public void OnGet()
        {
        }
    }
}
