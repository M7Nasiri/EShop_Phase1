using AutoMapper;
using EShop.Application.Interfaces;
using EShop.Domain.Dtos.ProductAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Admin
{
    [Authorize]
    public class ProductManagementModel(IProductService _productService, IMapper _mapper) : PageModel
    {
        [BindProperty]
        public List<ShowProductDto> Products { get; set; }
        public void OnGet()
        {
            Products = _productService.GetAllProductsForShow();
        }
    }
}
