using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models.Group
{
    public class JoinRequestInsertDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
