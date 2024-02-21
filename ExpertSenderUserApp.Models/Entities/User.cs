using System.ComponentModel.DataAnnotations;

namespace ExpertSenderUserApp.Models.Entities
{
    public class User : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
