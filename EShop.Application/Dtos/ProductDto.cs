using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public long UnitCost { get; set; } 
        public int Stock { get; set; }
    }

}
