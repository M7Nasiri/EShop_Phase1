using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.ViewModels.ProductAgg
{
    public class ShowProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImagePath { get; set; }
        public long UnitCost { get; set; }
        public int Stock { get; set; }
        public int Rate { get; set; }
        public bool IsInSlider { get; set; }
        public bool IsDelete { get; set; }
    }
}
