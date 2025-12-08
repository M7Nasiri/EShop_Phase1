using EShop.Application.Interfaces;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Services
{
    public class CategoryService(ICategoryRepositroy _categoryRepository) : ICategoryService
    {
        public bool Create(Category category)
        {
            return _categoryRepository.Create(category);
        }

        public bool Delete(int id)
        {
            var res =  _categoryRepository.Delete(id);
            if (!res)
            {
                throw new Exception("دسته بندی شامل محصول می باشد .");
            }
            return res;
        }

        public Category? Get(int categoryId)
        {
            return _categoryRepository.Get(categoryId);
        }

        public List<Category> GetAll()
        {
           return _categoryRepository.GetAll();
        }

        public bool Update(int id, Category category)
        {
            return _categoryRepository.Update(id, category);
        }
    }
}
