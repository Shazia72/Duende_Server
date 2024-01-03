using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? AccessToken { get; set; }
        public string? UserPhone { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Post>? Posts { get; set; }

    }
}
