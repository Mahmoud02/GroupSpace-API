using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.GroupSpace.Entities
{
    public class User : IConcurrencyAware
    {
        [Key]       
        public Guid Id { get; set; }

        [MaxLength(200)]
        [Required]
        [Column(TypeName = "text")]
        public string Subject { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "text")]
        public string Username { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "text")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public bool Active { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "text")]
        public string Email { get; set; }

        [ConcurrencyCheck]      
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Column(TypeName = "text")]
        [MaxLength(200)]
        public string SecurityCode { get; set; }

        public DateTime SecurityCodeExpirationDate { get; set; }

        public ICollection<UserClaim> Claims { get; set; } = new List<UserClaim>();
    }
}
