using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasQueryFilter(p => !p.IsDelete);

            builder.HasData(
                new Product { Id = 1, Title = "لباس ورزشی", Description = "لباس مناسب ورزش روزانه", UnitCost = 350000, Stock = 15, Rate = 4, ImagePath = "/Products/1.jpg",IsInSlider = true, IsDelete = false },
            new Product { Id = 2, Title = "کفش اسپرت", Description = "کفش حرفه‌ای راحتی", UnitCost = 780000, Stock = 8, Rate = 5, ImagePath = "/Products/2.jpg", IsInSlider = true, IsDelete = false },
            new Product { Id = 3, Title = "ساعت هوشمند", Description = "ساعت هوشمند با امکانات کامل", UnitCost = 1250000, Stock = 5, Rate = 4, ImagePath = "/Products/3.jpg", IsInSlider = true, IsDelete = false },
            new Product { Id = 4, Title = "هندزفری بیسیم", Description = "کیفیت بالا و باتری مناسب", UnitCost = 420000, Stock = 25, Rate = 3, ImagePath = "/Products/4.jpg", IsInSlider = false, IsDelete = false },
            new Product { Id = 5, Title = "کوله پشتی", Description = "مناسب سفر و دانشگاه", UnitCost = 260000, Stock = 12, Rate = 4, ImagePath = "/Products/5.jpg", IsInSlider = false, IsDelete = false },
            new Product { Id = 6, Title = "کیبورد گیمینگ", Description = "کیبورد RGB حرفه‌ای", UnitCost = 690000, Stock = 7, Rate = 5, ImagePath = "/Products/6.jpg", IsInSlider = false, IsDelete = false },
            new Product { Id = 7, Title = "ماوس گیمینگ", Description = "ماوس با حساسیت بالا", UnitCost = 350000, Stock = 18, Rate = 4, ImagePath = "/Products/7.jpg", IsInSlider = false, IsDelete = false },
            new Product { Id = 8, Title = "مانیتور 24 اینچ", Description = "مانیتور Full HD", UnitCost = 3200000, Stock = 4, Rate = 5, ImagePath = "/Products/8.jpg", IsInSlider = false, IsDelete = false },
            new Product { Id = 9, Title = "پاوربانک", Description = "قابلیت شارژ سریع", UnitCost = 550000, Stock = 20, Rate = 4, ImagePath = "/Products/9.jpg", IsInSlider = false, IsDelete = false },
            new Product { Id = 10, Title = "هدفون حرفه‌ای", Description = "کیفیت صدای بالا", UnitCost = 980000, Stock = 6, Rate = 5, ImagePath = "/Products/10.jpg", IsInSlider = false, IsDelete = false }
        );
        }
    }
}
