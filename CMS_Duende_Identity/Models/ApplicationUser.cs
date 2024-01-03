using Microsoft.AspNetCore.Identity;

namespace CMS_Duende_Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
