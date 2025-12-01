using EShop.Domain.Enum;

namespace EShop.Domain.ViewModels.UserAgg
{
    public class UserInfoForAdmin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public RoleEnum Role { get; set; }
        public int WriterPostCount { get; set; }
        public int VerifierPostCount { get; set; }
    }
}
