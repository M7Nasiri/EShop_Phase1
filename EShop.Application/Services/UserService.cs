using EShop.Application.Interfaces;
using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Claims;


namespace EShop.Application.Services
{
    public class UserService(IUserRepository userRepository,ILogger<UserService> _logger) : IUserService
    {
        public int CreateUserByAdmin(int adminId, CreateUserByAdminDto create)
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

        public bool DeleteUserByAdmin(int adminId, DeleteUserByAdminDto delete)
        {
            return userRepository.DeleteUserByAdmin(adminId, delete);
        }

        public int FindIdByUserName(string userName)
        {
            return userRepository.FindIdByUserName(userName);
        }

        public List<GetUserDto> GetAll()
        {
            return userRepository.GetAll();
        }

        public GetUserDto? GetUserById(int id)
        {
            return userRepository.GetUserById(id);
        }

        public List<UserInfoForAdminDto> GetUserInfosForAdmin(int userId)
        {
            return userRepository.GetUserInfosForAdmin(userId);
        }

        public bool IsUserExist(string userName)
        {
            return userRepository.IsUserExist(userName);
        }

        //public GetUserDto? Login(LoginUserDto login)
        //{
        //    return userRepository.Login(login);
        //}

        public bool Register(RegisterUserDto register)
        {
            return userRepository.Register(register);
        }

        //public bool UpdatePassword(int id, UpdatePasswordDto model)
        //{
        //    return userRepository.UpdatePassword(id, model);
        //}

        //public bool UpdateRememberMe(int id, bool rememberMe)
        //{
        //    return userRepository.UpdateRememberMe(id, rememberMe);
        //}

        public bool UpdateUserByAdmin(int adminId, int id, UpdateUserByAdminDto model)
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
            if(remain < 10000)
            {
                _logger.LogWarning("موجودی کیف پول کاربر به کمتر از ده هزار توامن رسیده است");
            }
            UpdateUserWallet(userId, remain);
        }

        public List<GetUserDto> GetAllNotCurrent(int currentId)
        {
            return userRepository.GetAllNotCurrent(currentId);
        }

        public GetUserOrdersDto GetUserOrders(int id)
        {
            return userRepository.GetUserOrders(id);
        }

        public UserInfoDto GetUserInfo(int userId)
        {
            return userRepository.GetUserInfo(userId);    
        }

        public void SetUserInfo(int userId, long credit, string fullName)
        {
            userRepository.SetUserInfo(userId, credit, fullName); 
        }
    }
}
