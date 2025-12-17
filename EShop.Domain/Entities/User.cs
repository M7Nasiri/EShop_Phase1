using EShop.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace EShop.Domain.Entities
{
    public class User
    {

        //public int Id { get; set; }
        [Key]
        public int IdentityUserId { get; set; }
        [ForeignKey(nameof(IdentityUserId))]
        public IdentityUser<int> IdentityUser { get; set; }
        public string UserName { get; set; }
       // public string Password { get; set; }
        public string? FullName { get; set; }
        public List<Order>? Orders { get; set; }
        public long Credit { get; set; }
        //public RoleEnum Role { get; set; }
        public bool IsDelete { get; set; }

   

    }
}
