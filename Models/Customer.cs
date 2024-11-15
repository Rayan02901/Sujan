using System.ComponentModel.DataAnnotations;

namespace FlareTech.Models
{
    public class Customer : BasePerson
    {
        [Required(ErrorMessage = "Company name is required")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
