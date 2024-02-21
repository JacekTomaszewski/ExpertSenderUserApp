using System.ComponentModel.DataAnnotations;

namespace ExpertSenderUserApp.Models.Entities
{
    public class EntityBase
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
    }
}
