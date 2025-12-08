using EShop.Domain.Enum;

namespace EShop.Domain.Dtos.UserAgg
{
    public class CreateUserByAdminDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
    }
}
