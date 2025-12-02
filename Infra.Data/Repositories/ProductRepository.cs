using AutoMapper;
using EShop.Domain.common;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.ProductAgg;
using Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Repositories
{
    public class ProductRepository(AppDbContext _context,IMapper mapper) : IProductRepository
    {
        public List<ShowProductViewModel> GetAllProductsForShow()
        {
            return mapper.Map<List<ShowProductViewModel>>(_context.Products.Include(p => p.Category).ToList());
        }

        public ProductDetailsViewModel GetProductDetailsById(int id)
        {
            return mapper.Map<ProductDetailsViewModel>(_context.Products.Include(p => p.Category).Where(p => p.Id == id)
                .FirstOrDefault());
        }

        public List<ShowProductViewModel> GroupingByCategory(GroupingByCategory grouping)
        {
            IQueryable<Product> iQuery = _context.Products.Include(p => p.Category);
            if(grouping.CategoryId != 0)
            {
                iQuery = iQuery.Where(p => p.CategoryId == grouping.CategoryId);
            }
            return mapper.Map<List<ShowProductViewModel>>(iQuery);
        }

        public ResultDto<int> HasEnoughStock(int id,int sellingCount)
        {
            var result = new ResultDto<int>();
            int stock = _context.Products.Where(p => p.Id == id).Select(p=>p.Stock).FirstOrDefault();
            result.IsSuccess =  stock >= sellingCount;
            result.Data = stock;
            return result;
        }

        public void UpdateStock(int id,int stock, int sellingCount)
        {
            _context.Products.Where(p => p.Id == id).ExecuteUpdate(setters => setters.SetProperty((p => p.Stock), stock - sellingCount));
        }
    }
}
