using System;

namespace GroupSpace.BLL.Models
{
    public class PostDto
    {
        public int PostId { get; set; }
        public String Text { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int NumOfLikes { get; set; }
        public String PhotoUrl { get; set; }
        public DateTime Date { get; set; }
        public UserDto User { get; set; }
    }
}
