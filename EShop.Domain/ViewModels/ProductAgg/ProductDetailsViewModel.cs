using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.ViewModels.ProductAgg
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImagePath { get; set; }
        public long UnitCost { get; set; }
        public int Stock { get; set; }
        public int Rate { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
