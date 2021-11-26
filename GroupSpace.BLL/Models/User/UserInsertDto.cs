using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models.User
{
    public  class UserInsertDto
    {
        [MaxLength(200)]
        [Required]
        public string  SubID { get; set; }
        [Required]
        public string UserName { get; set; }
        public string PersonalImageUrl { get; set; }
        public string Bio { get; set; }
    }
}
