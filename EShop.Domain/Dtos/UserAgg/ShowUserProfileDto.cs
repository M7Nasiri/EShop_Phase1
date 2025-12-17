using EShop.Domain.Dtos.OrderAgg;
using EShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Dtos.UserAgg
{
    public class ShowUserPrfofileDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserInfoDto UserInfo { get; set; }
        public List<GetOrderDto> Orders { get; set; }

    }
}
