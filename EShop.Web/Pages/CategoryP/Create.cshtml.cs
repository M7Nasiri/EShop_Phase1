using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.CategoryP
{
    public class CreateModel(ICategoryService _categoryService) : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _categoryService.Create(Category);
            return RedirectToPage("/CategoryP/Index");
        }
    }
}
