using EShop.Domain.ViewModels.ProductAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Product
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public DeleteProductViewModel Product { get; set; }
        public void OnGet()
        {
        }
    }
}
