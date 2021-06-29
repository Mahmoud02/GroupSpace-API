using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models.Group
{
    public class JoinRequestDto
    {
        public int JoinRequestId { get; set; }
        public DateTime Date { get; set; }
        public UserDto User { get; set; }
    }
}
