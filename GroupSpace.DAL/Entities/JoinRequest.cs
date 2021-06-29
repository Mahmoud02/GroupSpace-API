using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Entities
{
    public class JoinRequest
    {
        public int JoinRequestId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }      
        [Required]
        public DateTime Date { get; set; }
        public User User { get; set; }

    }
}
