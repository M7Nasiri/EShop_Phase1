using EShop.Domain.Enum;

namespace EShop.Domain.ViewModels.UserAgg
{
    public class CreateUserByAdmin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
    }
}
