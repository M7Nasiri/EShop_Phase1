using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public long UnitCost { get; set; }
        public int Stock { get; set; }
        public int Rate { get; set; }
        public bool IsInSlider { get; set; }
        public bool IsDelete { get; set; }
    }
}
