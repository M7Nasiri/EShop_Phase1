using EShop.Domain.ViewModels.UserAgg;

namespace EShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        List<GetUserViewModel> GetAll();
        GetUserViewModel? GetUserById(int id);
        GetUserViewModel? Login(LoginUserViewModel login);
        bool Register(RegisterUserViewModel register);
        bool IsUserExist(string userName);
        bool Delete(int id);
        bool UpdatePassword(int id, UpdatePasswordUserViewModel model);
        int FindIdByUserName(string userName);
        bool UpdateUserByAdmin(int adminId, int id, UpdateUserByAdminViewModel model);
        bool UpdateRememberMe(int id, bool rememberMe);
        int CreateUserByAdmin(int adminId, CreateUserByAdmin create);
        bool DeleteUserByAdmin(int adminId, DeleteUserByAdmin delete);
        List<UserInfoForAdmin> GetUserInfosForAdmin(int userId);

    }
}
