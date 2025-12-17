using EShop.Application.Interfaces;
using EShop.Domain.Dtos.ProductAgg;
using EShop.Domain.Entities;
using EShop.Web.Services;
using EShop.Web.ViewModels.ProductAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;

namespace EShop.Web.Pages.Product
{
    public class CreateModel(IProductService _productService,ICategoryService _categoryService,IWorkWithProductImage _wImage) : PageModel
    {
        [BindProperty]
        public AddProductViewModel Product { get; set; }
        [BindProperty]
        public List<Category> Categories { get; set; }

        public void OnGet()
        {
            Categories = _categoryService.GetAll();
            //throw new Exception("Error in create product");
        }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                Categories = _categoryService.GetAll();
                return Page();
            }
            var path = _wImage.Upload(Product.ImageFile);
            var dto = new AddProductDto
            {
                CategoryId = Product.CategoryId,
                Description = Product.Description,
                IsInSlider = Product.IsInSlider,
                Stock = Product.Stock,
                Title = Product.Title,
                UnitCost = Product.UnitCost,
                ImagePath = path
            };
            _productService.Create(dto);
            return RedirectToPage("/Admin/ProductManagement");
        }
    }
}
