using EShop.Domain.common;
using EShop.Domain.Dtos.ProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        ResultDto<int> HasEnoughStock(int id, int sellingCount);
        ProductDetailsDto GetProductDetailsById(int id);
        List<ShowProductDto> GetAllProductsForShow();

        void UpdateStock(int id, int stock, int sellingCount);
        List<ShowProductDto> GroupingByCategory(GroupingByCategoryDto grouping);

        void Delete(int id);
        void Update(int id ,UpdateProductDto dto);
        string GetImagePath(int id);

        int Create(AddProductDto dto);
    }
}
