using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Entities;

namespace EShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        List<GetUserDto> GetAll();
        List<GetUserDto> GetAllNotCurrent(int currentId);
        GetUserDto? GetUserById(int id);
       // GetUserDto? Login(LoginUserDto login);
        bool Register(RegisterUserDto register);
        bool IsUserExist(string userName);
        bool Delete(int id);
       // bool UpdatePassword(int id, UpdatePasswordDto model);
        int FindIdByUserName(string userName);
        bool UpdateUserByAdmin(int adminId, int id, UpdateUserByAdminDto model);
        //bool UpdateRememberMe(int id, bool rememberMe);
        int CreateUserByAdmin(int adminId, CreateUserByAdminDto create);
        bool DeleteUserByAdmin(int adminId, DeleteUserByAdminDto delete);
        List<UserInfoForAdminDto> GetUserInfosForAdmin(int userId);
        long GetUserWallet(int userId);
        void UpdateUserWallet(int userId,long remain);
        GetUserOrdersDto GetUserOrders(int id);
        UserInfoDto GetUserInfo(int userId);

        void SetUserInfo(int userId,long credit,string fullName);

    }
}
