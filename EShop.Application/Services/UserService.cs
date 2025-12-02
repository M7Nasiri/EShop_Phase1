using EShop.Application.Interfaces;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.UserAgg;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EShop.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public int CreateUserByAdmin(int adminId, CreateUserByAdmin create)
        {
            if (IsUserExist(create.UserName))
            {
                return 0;
            }
            return userRepository.CreateUserByAdmin(adminId, create);
        }

        public bool Delete(int id)
        {
            return userRepository.Delete(id);
        }

        public bool DeleteUserByAdmin(int adminId, DeleteUserByAdmin delete)
        {
            return userRepository.DeleteUserByAdmin(adminId, delete);
        }

        public int FindIdByUserName(string userName)
        {
            return userRepository.FindIdByUserName(userName);
        }

        public List<GetUserViewModel> GetAll()
        {
            return userRepository.GetAll();
        }

        public GetUserViewModel? GetUserById(int id)
        {
            return userRepository.GetUserById(id);
        }

        public List<UserInfoForAdmin> GetUserInfosForAdmin(int userId)
        {
            return userRepository.GetUserInfosForAdmin(userId);
        }

        public bool IsUserExist(string userName)
        {
            return userRepository.IsUserExist(userName);
        }

        public GetUserViewModel? Login(LoginUserViewModel login)
        {
            return userRepository.Login(login);
        }

        public bool Register(RegisterUserViewModel register)
        {
            return userRepository.Register(register);
        }

        public bool UpdatePassword(int id, UpdatePasswordUserViewModel model)
        {
            return userRepository.UpdatePassword(id, model);
        }

        public bool UpdateRememberMe(int id, bool rememberMe)
        {
            return userRepository.UpdateRememberMe(id, rememberMe);
        }

        public bool UpdateUserByAdmin(int adminId, int id, UpdateUserByAdminViewModel model)
        {
            var user = GetUserById(id);
            if (model.Password == null)
            {
                model.Password = user.Password;
            }
            return userRepository.UpdateUserByAdmin(adminId, id, model);
        }
        public int GetCurrentUserId(ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public long GetUserWallet(int userId)
        {
            return userRepository.GetUserWallet(userId);
        }

        public bool ISCreditSufficient(long credit, long cost)
        {
            return credit >= cost;
        }
        public void UpdateUserWallet(int userId, long remain)
        {
            userRepository.UpdateUserWallet(userId, remain); 
        }
        public void DecreaseWallet(int userId, long totalPrice)
        {
            long remain = GetUserWallet(userId) - totalPrice;
            UpdateUserWallet(userId, remain);
        }
    }
}
