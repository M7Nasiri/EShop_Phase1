using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.CategoryP
{
    public class IndexModel(ICategoryService _categoryService) : PageModel
    {
        public List<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = _categoryService.GetAll();
        }
    }
}
