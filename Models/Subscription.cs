using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace FlareTech.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int PlanId { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "Monthly Fee")]
        [DataType(DataType.Currency)]
        public decimal MonthlyFee { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }

}
