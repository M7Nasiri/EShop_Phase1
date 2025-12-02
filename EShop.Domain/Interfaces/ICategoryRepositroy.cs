using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Interfaces
{
    public interface ICategoryRepositroy
    {
        List<Category> GetAll();
    }
}
