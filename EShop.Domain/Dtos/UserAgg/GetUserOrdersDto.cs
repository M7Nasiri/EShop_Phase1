using EShop.Domain.Entities;
using EShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Dtos.UserAgg
{
    public class GetUserOrdersDto
    {

        public int IdentityUserId { get; set; }
        public string UserName { get; set; }

        public List<Order>? Orders { get; set; }
        public int Credit { get; set; }
    }
}
