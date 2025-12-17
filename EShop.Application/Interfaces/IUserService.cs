using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Entities;
using System.Security.Claims;

namespace EShop.Application.Interfaces
{
    public interface IUserService
    {
        List<GetUserDto> GetAll();
        GetUserDto? GetUserById(int id);
       // GetUserDto? Login(LoginUserDto login);
        bool Register(RegisterUserDto register);
        bool IsUserExist(string userName);
        bool Delete(int id);
       // bool UpdatePassword(int id, UpdatePasswordDto model);
        int FindIdByUserName(string userName);
        bool UpdateUserByAdmin(int adminId, int id, UpdateUserByAdminDto model);
       // bool UpdateRememberMe(int id, bool rememberMe);
       //int CreateUserByAdmin(int adminId, CreateUserByAdminDto create);
        bool DeleteUserByAdmin(int adminId, DeleteUserByAdminDto delete);

        List<UserInfoForAdminDto> GetUserInfosForAdmin(int userId);
        int GetCurrentUserId(ClaimsPrincipal user);
        long GetUserWallet(int userId);
        bool ISCreditSufficient(long credit, long cost);
        void DecreaseWallet(int userId, long totalPrice);
        void UpdateUserWallet(int userId, long remain);
        List<GetUserDto> GetAllNotCurrent(int currentId);
        GetUserOrdersDto GetUserOrders(int id);
        UserInfoDto GetUserInfo(int userId);

        void SetUserInfo(int userId, long credit, string fullName);
    }
}
