using System.ComponentModel.DataAnnotations;
using testy.Common;

namespace testy.Domain.Entities
{
    public class ContactMaster : AuditableEntity
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [StringLength(100), Required]
        public string Email { get; set;}
    }
}
