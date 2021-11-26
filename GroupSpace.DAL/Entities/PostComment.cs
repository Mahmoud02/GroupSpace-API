using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Entities
{
    public class PostComment
    {
        public int PostCommentId { get; set; }
        [Column(TypeName = "text")]
        public String Text { get; set; }
        [Required]
        public int PostId { get; set; }
        public int UserId { get; set; }       
        [Required]
        public DateTime Date { get; set; }
        public int NumOfLikes { get; set; }
        public User User { get; set; }
    }
}
