using EShop.Domain.Entities;
using EShop.Domain.Enum;

namespace EShop.Domain.Dtos.UserAgg
{
    public class UserInfoForAdminDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public RoleEnum Role { get; set; }
        public int OrdersCount { get; set; }
        public long Credit { get; set; }

    }
}
