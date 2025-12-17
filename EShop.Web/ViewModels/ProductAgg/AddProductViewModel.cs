using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShop.Web.ViewModels.ProductAgg
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "عنوان الزامی است")]
        [MaxLength(100)]
        [MinLength(3, ErrorMessage = "حداقل طول عنوان باید 3 باشد .")]
        public string Title { get; set; }
        [MaxLength(500)]
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
