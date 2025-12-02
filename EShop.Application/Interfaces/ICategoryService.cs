using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }
}
