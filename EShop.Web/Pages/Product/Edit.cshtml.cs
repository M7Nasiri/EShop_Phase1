using AutoMapper;
using EShop.Application.Interfaces;
using EShop.Domain.Dtos.ProductAgg;
using EShop.Domain.Entities;
using EShop.Web.Services;
using EShop.Web.ViewModels.ProductAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Product
{
    public class EditModel(IProductService _productService,IMapper _mapper,ICategoryService _categoryService,IWorkWithProductImage _wImage) : PageModel
    {
        [BindProperty]
        public UpdateProductViewModel Product { get; set; }
        [BindProperty]
        public List<Category> Categories { get; set; }
        public void OnGet(int id)
        {
            var dto = _productService.GetProductDetailsById(id);
            Product = new UpdateProductViewModel
            {
                UnitCost = dto.UnitCost,
                Title = dto.Title,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                Id = id,
                ImagePath = dto.ImagePath,
                IsInSlider = dto.IsInSlider,
                Stock = dto.Stock
            };
                
            Categories = _categoryService.GetAll();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var path = _wImage.Upload(Product.ImageFile,id);
            var dto = new UpdateProductDto
            {
                CategoryId = Product.CategoryId,
                Description = Product.Description,
                Id = id,
                ImagePath = path,
                IsInSlider = Product.IsInSlider,
                Stock = Product.Stock,
                Title = Product.Title,
                UnitCost = Product.UnitCost,
            };
            _productService.Update(id, dto);
            return RedirectToPage("/Admin/ProductManagement");
        }
    }
}
