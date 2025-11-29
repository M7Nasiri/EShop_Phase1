using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.ViewModels.ProductAgg
{
    public class AddProductViewModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public long UnitCost { get; set; }
        public int Stock { get; set; }
        public bool IsInSlider { get; set; }

    }
}
