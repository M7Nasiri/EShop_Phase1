using EShop.Domain.Enum;


namespace EShop.Domain.ViewModels.UserAgg
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public RoleEnum Role { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
