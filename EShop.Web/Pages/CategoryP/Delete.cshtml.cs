using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.CategoryP
{
    public class DeleteModel(ICategoryService _categoryService) : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = _categoryService.Get(id);
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _categoryService.Delete(id);
                return RedirectToPage("/CategoryP/Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return Page();
        }
    }
}
