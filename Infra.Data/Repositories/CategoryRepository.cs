using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;
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
        public bool Create(Category category)
        {
            _context.Add(category);
            return _context.SaveChanges() > 0;
        }

        public Category? Get(int categoryId)
        {
            return _context.Cateogries.FirstOrDefault(c => c.Id == categoryId);
        }


        public bool Update(int id, Category model)
        {
            var cat = _context.Cateogries.FirstOrDefault(c => c.Id == id);
            if (cat != null)
            {
                cat.Title = model.Title;
                cat.Description = model.Description;
            }
            return _context.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var category = _context.Cateogries.Include(c => c.Products).Where(c => c.Id == id).FirstOrDefault();
            if (category != null && category.Products.Count() == 0 )
                return _context.Cateogries.Where(c => c.Id == id).
                    ExecuteUpdate(setters => setters.SetProperty((c => c.IsDelete), true)) > 0;
            return false;
            //return _context.Cateogries.Where(u => u.Id == id).ExecuteUpdate(setters=>setters.SetProperty((u=>u.IsDelete),true)) > 0;
        }
    }
}
