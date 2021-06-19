using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string UserName { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Email { get; set; }
        [Column(TypeName = "text")]
        public string PersonalImageUrl { get; set; }
        [Column(TypeName = "text")]
        public string Bio { get; set; }
        public bool OnlineStatus { get; set; }
        [Column(TypeName = "text")]
        public string Token { get; set; }
        public List<Group> UserGroups { get; set; }


    }
}
