using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Dtos.ProductAgg
{
    public class ShowProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImagePath { get; set; }
        public long UnitCost { get; set; }
        public int Stock { get; set; }
        public int Rate { get; set; }
        public bool IsInSlider { get; set; }
        public bool IsDelete { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
