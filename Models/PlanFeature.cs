using System.ComponentModel.DataAnnotations;

namespace FlareTech.Models
{
    public class PlanFeature
    {
        [Key]
        public int PlanFeatureId { get; set; }

        public int PlanId { get; set; }
        public int FeatureId { get; set; }

        [Required]
        public bool IsIncluded { get; set; }

        // Navigation properties
        public virtual Plan Plan { get; set; }
        public virtual Feature Feature { get; set; }
    }
}
