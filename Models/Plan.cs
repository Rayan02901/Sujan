using System.ComponentModel.DataAnnotations;

namespace FlareTech.Models
{
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }

        [Required(ErrorMessage = "Plan name is required")]
        [Display(Name = "Plan Name")]
        public string PlanName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Base Price")]
        [DataType(DataType.Currency)]
        public decimal BasePrice { get; set; }

        // Navigation properties
        public virtual ICollection<PlanFeature> PlanFeatures { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
