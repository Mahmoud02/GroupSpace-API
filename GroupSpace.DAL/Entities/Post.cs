using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        [Column(TypeName = "text")]
        public String Text { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public int NumOfLikes { get; set; }
        [Column(TypeName = "text")]
        public String PhotoUrl { get; set; }
        [Required]
        public DateTime Date { get; set; }
        //
        public User User { get; set; }

    }
}
