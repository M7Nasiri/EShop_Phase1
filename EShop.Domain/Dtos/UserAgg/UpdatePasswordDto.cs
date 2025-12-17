namespace EShop.Domain.Dtos.UserAgg
{
    public class UpdatePasswordDto
    {
        public int IdentityUserId { get; set; }
        public string Password { get; set; }
    }
}
