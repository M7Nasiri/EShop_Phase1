using AutoMapper;
using EShop.Domain.common;
using EShop.Domain.Dtos.ProductAgg;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Repositories
{
    public class ProductRepository(AppDbContext _context,IMapper mapper) : IProductRepository
    {
        public int Create(AddProductDto dto)
        {
            var product = mapper.Map<Product>(dto);
            _context.Products.Add(product);
            return _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Products.Where(p=>p.Id == id).ExecuteUpdate(setters=>setters.SetProperty((p=>p.IsDelete),true));
        }

        public List<ShowProductDto> GetAllProductsForShow()
        {
            return mapper.Map<List<ShowProductDto>>(_context.Products.Include(p => p.Category).ToList());
        }

        public string GetImagePath(int id)
        {
            return _context.Products.Where(p => p.Id == id).Select(p => p.ImagePath).FirstOrDefault();
        }

        public ProductDetailsDto GetProductDetailsById(int id)
        {
            return mapper.Map<ProductDetailsDto>(_context.Products.Include(p => p.Category).Where(p => p.Id == id)
                .FirstOrDefault());
        }

        public List<ShowProductDto> GroupingByCategory(GroupingByCategoryDto grouping)
        {
            IQueryable<Product> iQuery = _context.Products.Include(p => p.Category);
            if(grouping.CategoryId != 0)
            {
                iQuery = iQuery.Where(p => p.CategoryId == grouping.CategoryId);
            }
            return mapper.Map<List<ShowProductDto>>(iQuery);
        }

        public ResultDto<int> HasEnoughStock(int id,int sellingCount)
        {
            var result = new ResultDto<int>();
            int stock = _context.Products.Where(p => p.Id == id).Select(p=>p.Stock).FirstOrDefault();
            result.IsSuccess =  stock >= sellingCount;
            result.Data = stock;
            return result;
        }

        public void Update(int id, UpdateProductDto dto)
        {
            _context.Products.Where(propa => propa.Id == id).ExecuteUpdate(setters => setters
            .SetProperty((p => p.Title), dto.Title)
            .SetProperty((p => p.Description), dto.Description)
            .SetProperty((p => p.ImagePath), dto.ImagePath)
            .SetProperty((p => p.UnitCost), dto.UnitCost)
            .SetProperty((p => p.Stock), dto.Stock)
            .SetProperty((p => p.IsInSlider), dto.IsInSlider)
            .SetProperty((p => p.CategoryId), dto.CategoryId));
        }

        public void UpdateStock(int id,int stock, int sellingCount)
        {
            _context.Products.Where(p => p.Id == id).ExecuteUpdate(setters => setters.SetProperty((p => p.Stock), stock - sellingCount));
        }
    }
}
