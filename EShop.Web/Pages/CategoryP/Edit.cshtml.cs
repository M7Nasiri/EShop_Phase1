using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.CategoryP
{
    public class EditModel(ICategoryService _categoryService) : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = _categoryService.Get(id);
        }
        public IActionResult OnPost(int id)
        {
            _categoryService.Update(id, Category);
            return RedirectToPage("/CategoryP/Index");
        }
    }
}
