using EShop.Domain.Enum;

namespace EShop.Domain.ViewModels.UserAgg
{
    public class DeleteUserByAdmin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public RoleEnum Role { get; set; }
        public bool IsDelete { get; set; }
    }
}
