using EShop.Domain.Enum;

namespace EShop.Web.ViewModels.UserAgg
{
    public class GetUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string FullName { get; set; }

        public RoleEnum Role { get; set; }
    }
}
