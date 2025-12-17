using EShop.Domain.Enum;

namespace EShop.Domain.Dtos.UserAgg
{
    public class UpdateUserByAdminDto
    {
        public int IdentityUserId { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public RoleEnum Role { get; set; }
    }
}
