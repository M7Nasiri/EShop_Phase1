using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Entities
{
    public class UserCartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
    }

}
