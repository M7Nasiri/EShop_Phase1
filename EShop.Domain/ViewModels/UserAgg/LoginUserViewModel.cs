
using EShop.Domain.Enum;

namespace EShop.Domain.ViewModels.UserAgg
{
    public class LoginUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
