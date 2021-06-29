using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Entities
{
    public class ReportPost
    {
        public int ReportPostId { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int NumOfTimes { get; set; }

        //Navigation
        public User User { get; set; }
        public Post Post { get; set; }

    }
}
