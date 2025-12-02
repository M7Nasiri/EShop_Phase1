using EShop.Application.Interfaces;
using EShop.Domain.common;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.ProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Services
{
    public class ProductService(IProductRepository _productRepository) : IProductService
    {
        public List<ShowProductViewModel> GetAllProductsForShow()
        {
            return _productRepository.GetAllProductsForShow();
        }

        public ProductDetailsViewModel GetProductDetailsById(int id)
        {
            return _productRepository.GetProductDetailsById(id);
        }

        public List<ShowProductViewModel> GroupingByCategory(GroupingByCategory grouping)
        {
            return _productRepository.GroupingByCategory(grouping);
        }

        public ResultDto<int> HasEnoughStock(int id, int sellingCount)
        {
            return _productRepository.HasEnoughStock(id, sellingCount);
        }

        public void UpdateStock(int id, int stock, int sellingCount)
        {
            _productRepository.UpdateStock(id,stock, sellingCount); 
        }
    }
}
