using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models.Group
{
    public class ReportPostDto
    {
        public int ReportPostId { get; set; }
        public int NumOfTimes { get; set; }
        public UserDto User { get; set; }
        public PostDto Post { get; set; }
    }
}
