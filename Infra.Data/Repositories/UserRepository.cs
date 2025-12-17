using AutoMapper;
using EShop.Domain.Dtos.UserAgg;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class UserRepository(AppDbContext context, IMapper mapper) : IUserRepository
    {
        public int CreateUserByAdmin(int adminId, CreateUserByAdminDto create)
        {
            var user = mapper.Map<User>(create);
            context.Add(user);
            context.SaveChanges();
          
            return user.Id;

        }

        public bool DeleteUserByAdmin(int adminId, DeleteUserByAdminDto delete)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == delete.Id);
            if (user != null)
            {
                return context.Users.Where(u => u.Id == delete.Id).ExecuteUpdate(setters => setters.SetProperty((u => u.IsDelete), true)) > 0;
            }
            return false;
        }
        public bool Delete(int id)
        {
            return context.Users.Where(u => u.Id == id).ExecuteUpdate(setters => setters.SetProperty((u => u.IsDelete), true)) > 0;
        }


        public int FindIdByUserName(string userName)
        {
            return context.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstOrDefault();
        }

        public List<GetUserDto> GetAll()
        {
            return mapper.Map<List<GetUserDto>>(context.Users.Include(u => u.Orders).ToList());
        }

        public GetUserDto? GetUserById(int id)
        {
            return mapper.Map<GetUserDto>(context.Users.Include(u=>u.Orders).FirstOrDefault(u => u.Id == id));
        }

        public bool IsUserExist(string userName)
        {
            return context.Users.Any(u => u.UserName == userName);
        }

        //public GetUserDto? Login(LoginUserDto login)
        //{
        //    var user = context.Users.Where(u => u.UserName == login.UserName && u.Password == login.Password).FirstOrDefault();
        //    if (user != null)
        //    {
        //        return mapper.Map<GetUserDto>(user);
        //    }
        //    return null;
        //}

        public bool Register(RegisterUserDto register)
        {
            var user = mapper.Map<User>(register);
            if (context.Users.Any(u => u.UserName == register.UserName))
                return false;
            context.Add(user);
            return context.SaveChanges() > 0;
        }

        //public bool UpdatePassword(int id, UpdatePasswordDto model)
        //{
        //    return context.Users.Where(u => u.Id == id)
        //                .ExecuteUpdate(setters => setters.SetProperty((u => u.Password), model.Password)) > 0;
        //}

        //public bool UpdateRememberMe(int id, bool rememberMe)
        //{
        //    return context.Users.Where(u => u.Id == id)
        //                .ExecuteUpdate(setters => setters.SetProperty((u => u.RememberMe), rememberMe)) > 0;
        //}

        public bool UpdateUserByAdmin(int adminId, int id, UpdateUserByAdminDto model)
        {
            return context.Users.Where(u => u.Id == id)
                         .ExecuteUpdate(setters => setters
                         .SetProperty((u => u.UserName), model.UserName)) > 0;
                        // .SetProperty((u => u.Password), model.Password)
                         //.SetProperty((u => u.Role), model.Role)) > 0;
        }

        public List<UserInfoForAdminDto> GetUserInfosForAdmin(int userId)
        {
            return mapper.Map<List<UserInfoForAdminDto>>(context.Users.Where(u => u.Id != userId)
                .ToList());
        }

        public long GetUserWallet(int userId)
        {
            return context.Users.Where(u => u.Id == userId).Select(x => x.Credit).FirstOrDefault();
        }

        public void UpdateUserWallet(int userId, long remain)
        {
            context.Users.Where(u => u.Id == userId).ExecuteUpdate(setters => setters
            .SetProperty((x => x.Credit), remain));
        }

        public List<GetUserDto> GetAllNotCurrent(int currentId)
        {
            return mapper.Map<List<GetUserDto>>(context.Users.Include(u=>u.Orders).Where(u=>u.Id !=currentId).ToList());
        }

        public GetUserOrdersDto GetUserOrders(int id)
        {
            return mapper.Map<GetUserOrdersDto>(context.Users.Where(u => u.IdentityUserId == id).Include(u => u.Orders)
                .ThenInclude(u => u.OrderItems).ThenInclude(oi=>oi.Product).FirstOrDefault());
        }

        public UserInfoDto GetUserInfo(int userId)
        {
            return context.Users.Where(u => u.IdentityUserId == userId).Select(u => new UserInfoDto
            {
                Credit = u.Credit,
                FullName = u.FullName
            }).FirstOrDefault();

        }

        public void SetUserInfo(int userId, long credit, string fullName)
        {
             context.Users.Where(u => u.IdentityUserId == userId).ExecuteUpdate(setters=>setters
            .SetProperty((u=>u.FullName),fullName)
            .SetProperty((u=>u.Credit),credit));
        }
    }
}
