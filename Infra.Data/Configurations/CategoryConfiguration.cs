using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasQueryFilter(c => !c.IsDelete);

            builder.HasData(
                new Category { Id = 1, Title = "محصولات دیجیتال", Description = "دسته بندی محصولات دیجیتال شامل موبایل و لبتاب" },
                new Category { Id = 2, Title = "پوشاک", Description = "پوشاک شامل کت وشلوار ، لباس های اسپرت" },
                new Category { Id = 3, Title = "محصولات آرایشی بهداشتی", Description = "محصولات آرایشی و بهداشتی " }
                );
        }
    }
}
