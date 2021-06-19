using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models
{
    public class UserInsertDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string PersonalImageUrl { get; set; }
        public string Bio { get; set; }
    }
}
