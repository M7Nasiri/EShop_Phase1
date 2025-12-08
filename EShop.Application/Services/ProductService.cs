using EShop.Application.Interfaces;
using EShop.Domain.common;
using EShop.Domain.Dtos.ProductAgg;
using EShop.Domain.Interfaces;

namespace EShop.Application.Services
{
    public class ProductService(IProductRepository _productRepository) : IProductService
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
            _productRepository.UpdateStock(id, stock, sellingCount);
        }
    }
}
