using Microsoft.AspNetCore.Identity;

namespace FlareTech.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Custom properties, if required
        public string FullName { get; set; }
    }
}
