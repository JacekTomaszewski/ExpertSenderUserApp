using System.ComponentModel.DataAnnotations;

namespace ExpertSenderUserApp.Models.DTOs
{
    public class UserDTO
    {
        [Required]
        public Guid Id { get; set; }
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
        public DateTime DateCreated { get; set; }
    }
}
