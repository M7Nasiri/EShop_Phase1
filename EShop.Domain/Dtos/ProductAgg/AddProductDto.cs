using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShop.Domain.Dtos.ProductAgg
{
    public class AddProductDto
    {
        [Required(ErrorMessage = "عنوان الزامی است")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public long UnitCost { get; set; }
        public int Stock { get; set; }
        public bool IsInSlider { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

    }
}
