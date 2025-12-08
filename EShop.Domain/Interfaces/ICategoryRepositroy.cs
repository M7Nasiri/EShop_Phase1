using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Interfaces
{
    public interface ICategoryRepositroy
    {
        List<Category> GetAll();
        Category? Get(int categoryId);
        bool Create(Category category);
        bool Update(int id, Category category);
        bool Delete(int id);
    }
}
