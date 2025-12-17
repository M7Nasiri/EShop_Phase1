using EShop.Domain.Entities;
using EShop.Domain.Enum;

namespace EShop.Domain.Dtos.UserAgg
{
    public class GetUserDto
    {
        public int IdentityUserId { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string FullName { get; set; }

        public RoleEnum Role { get; set; }
        public List<Order>? Orders { get; set; }
        public int Credit { get; set; }
    }
}
