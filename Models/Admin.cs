using System.ComponentModel.DataAnnotations;

namespace FlareTech.Models
{
    public class Admin : BasePerson
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        // Navigation property
        public virtual ICollection<AuditTrail> AuditTrails { get; set; }
    }
}
