using EShop.Domain.Enum;
using System.ComponentModel.DataAnnotations;


namespace EShop.Domain.Dtos.UserAgg
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "نام کاربری الزامی است")]
        [MaxLength(11)]
        [MinLength(11)]
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; }
        [Compare("Password",ErrorMessage = "پسورد و تکرارش ، مطابقت ندارند.")]
        public string RePassword { get; set; }
        public string Role { get; set; }
        public bool RememberMe { get; set; } = false;
        public string Email { get; set; }
        public int IdentityUserId { get; set; }
    }
}
