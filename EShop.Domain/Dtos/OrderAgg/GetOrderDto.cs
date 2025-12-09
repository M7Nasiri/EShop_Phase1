using EShop.Domain.Entities;
using EShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Dtos.OrderAgg
{
    public class GetOrderDto
    {

        public int Id { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public bool IsFinalized { get; set; }
        public long TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderStatus Status { get; set; }

    }
}
