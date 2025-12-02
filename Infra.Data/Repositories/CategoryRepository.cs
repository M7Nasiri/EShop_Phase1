using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using Infra.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Repositories
{
    public class CategoryRepository(AppDbContext _context) : ICategoryRepositroy
    {
        public List<Category> GetAll()
        {
            return _context.Cateogries.ToList();
        }
    }
}
