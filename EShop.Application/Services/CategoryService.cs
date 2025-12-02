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
        public List<Category> GetAll()
        {
           return _categoryRepository.GetAll();
        }
    }
}
