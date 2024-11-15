using System.ComponentModel.DataAnnotations;

namespace FlareTech.Models
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }

        [Required]
        [Display(Name = "Feature Name")]
        public string FeatureName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal AdditionalCost { get; set; }

        // Navigation property
        public virtual ICollection<PlanFeature> PlanFeatures { get; set; }
    }
}
