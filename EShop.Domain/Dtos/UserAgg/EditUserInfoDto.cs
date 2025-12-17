using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EShop.Domain.Dtos.UserAgg
{
    public class EditUserInfoDto
    {
        public string Email { get; set; }

        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }

        public string FullName { get; set; }
        public long Credit { get; set; }
    }
}
