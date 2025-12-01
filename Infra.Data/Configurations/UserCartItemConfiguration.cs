using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Configurations
{
    public class UserCartItemConfiguration : IEntityTypeConfiguration<UserCartItem>
    {
        public void Configure(EntityTypeBuilder<UserCartItem> builder)
        {
            builder.HasKey(x => x.Id); 

            builder.HasIndex(x => new { x.UserId, x.ProductId })
                   .IsUnique();

        }
    }
}
