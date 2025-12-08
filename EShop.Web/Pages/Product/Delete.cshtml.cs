using AutoMapper;
using EShop.Application.Interfaces;
using EShop.Domain.Dtos.ProductAgg;
using EShop.Web.ViewModels.ProductAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Product
{
    public class DeleteModel(IProductService _productService,IMapper _mapper) : PageModel
    {
        [BindProperty]
        public DeleteProductDto Product { get; set; }

        [BindProperty]
        public int ProductId { get; set; }

        public void OnGet(int id)
        {
            ProductId = id;
            Product = _mapper.Map<DeleteProductDto>(_productService.GetProductDetailsById(id));
        }

        public IActionResult OnPost(int id)
        {
            _productService.Delete(id);
            return RedirectToPage("/Admin/ProductManagement");
        }
    }
}
