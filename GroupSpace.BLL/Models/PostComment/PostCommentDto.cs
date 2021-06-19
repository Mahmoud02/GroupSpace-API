using GroupSpace.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models.PostComment
{
    public class PostCommentDto
    {
        public int PostCommentId { get; set; }
        public String Text { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int NumOfLikes { get; set; }
        public UserDto User { get; set; }
    }
}
