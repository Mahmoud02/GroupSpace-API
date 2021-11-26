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

        [MaxLength(200)]
        [Column(TypeName = "text")]
        public string SubID { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string UserName { get; set; }

        [Column(TypeName = "text")]
        public string PersonalImageUrl { get; set; }
        [Column(TypeName = "text")]
        public string Bio { get; set; }
    }
}
