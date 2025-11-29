using EShop.Domain.ViewModels.ProductAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages
{
    public class IndexModel() : PageModel
    {
        [BindProperty]
        public List<ShowProductViewModel> Products { get; set; }
        public void OnGet()
        {

        }
    }
}
