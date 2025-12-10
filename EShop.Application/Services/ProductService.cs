using EShop.Application.Interfaces;
using EShop.Domain.common;
using EShop.Domain.Dtos.ProductAgg;
using EShop.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EShop.Application.Services
{
    public class ProductService(IProductRepository _productRepository,ILogger<ProductService> _logger) : IProductService
    {
  
        public int Create(AddProductDto dto)
        {
            return _productRepository.Create(dto); 
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public List<ShowProductDto> GetAllProductsForShow()
        {
            return _productRepository.GetAllProductsForShow();
        }

        public string GetImagePath(int id)
        {
            return _productRepository.GetImagePath(id);
        }

        public ProductDetailsDto GetProductDetailsById(int id)
        {
            return _productRepository.GetProductDetailsById(id);
        }

        public List<ShowProductDto> GroupingByCategory(GroupingByCategoryDto grouping)
        {
            return _productRepository.GroupingByCategory(grouping);
        }

        public ResultDto<int> HasEnoughStock(int id, int sellingCount)
        {
            return _productRepository.HasEnoughStock(id, sellingCount);
        }

        public void Update(int id, UpdateProductDto dto)
        {
            _productRepository.Update(id, dto);
        }

        public void UpdateStock(int id, int stock, int sellingCount)
        {
            var productName = _productRepository.GetProductName(id);
            if(stock - sellingCount < 5)
            {
                //Log.Warning($"موجودی محصول {productName} به کمتر از 5 عدد رسیده است Serilog.");
                _logger.LogWarning($"موجودی محصول {productName} به کمتر از 5 عدد رسیده است .");
            }
            _productRepository.UpdateStock(id, stock, sellingCount);
        }
    }
}
