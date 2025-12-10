using EShop.Domain.Enum;
using System.ComponentModel.DataAnnotations;


namespace EShop.Domain.Dtos.UserAgg
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "نام کاربری الزامی است")]
        [MinLength(3,ErrorMessage = "طول نام کاربری باید حداقل 3 کاراکتر باشد .")]
        [MaxLength(10)]
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; }
        [Compare("Password",ErrorMessage = "پسورد و تکرارش ، مطابقت ندارند.")]
        public string RePassword { get; set; }
        public RoleEnum Role { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
