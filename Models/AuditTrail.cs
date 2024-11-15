using System.ComponentModel.DataAnnotations;

namespace FlareTech.Models
{
    public class AuditTrail
    {
        [Key]
        public int AuditId { get; set; }

        [Required]
        public int AdminId { get; set; }

        [Required]
        [Display(Name = "Action Type")]
        public string ActionType { get; set; }

        [Required]
        [Display(Name = "Entity Type")]
        public string EntityType { get; set; }

        [Required]
        [Display(Name = "Entity ID")]
        public string EntityId { get; set; }

        [Required]
        [Display(Name = "Action Date")]
        public DateTime ActionDate { get; set; } = DateTime.Now;

        [Required]
        public string Description { get; set; }

        // Navigation property
        public virtual Admin Admin { get; set; }
    }
}
