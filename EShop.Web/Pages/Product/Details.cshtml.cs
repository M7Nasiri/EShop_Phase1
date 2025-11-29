using EShop.Domain.ViewModels.ProductAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Product
{
    public class DetailsModel : PageModel
    {
        public ProductDetailsViewModel Product { get; set; }
        public void OnGet()
        {
        }
    }
}
