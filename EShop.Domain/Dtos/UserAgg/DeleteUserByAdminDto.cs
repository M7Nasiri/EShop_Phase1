using EShop.Domain.Enum;

namespace EShop.Domain.Dtos.UserAgg
{
    public class DeleteUserByAdminDto
    {
        public int IdentityUserId { get; set; }
        public string UserName { get; set; }
        public RoleEnum Role { get; set; }
        public bool IsDelete { get; set; }
    }
}
