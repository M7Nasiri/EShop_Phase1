using EShop.Domain.common;
using EShop.Domain.ViewModels.ProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        ResultDto<int> HasEnoughStock(int id, int sellingCount);
        ProductDetailsViewModel GetProductDetailsById(int id);
        List<ShowProductViewModel> GetAllProductsForShow();

        void UpdateStock(int id, int stock, int sellingCount);
        List<ShowProductViewModel> GroupingByCategory(GroupingByCategory grouping);
    }
}
