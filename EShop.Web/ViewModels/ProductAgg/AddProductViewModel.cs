using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Web.ViewModels.ProductAgg
{
    public class AddProductViewModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }
        public long UnitCost { get; set; }
        public int Stock { get; set; }
        public bool IsInSlider { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

    }
}
