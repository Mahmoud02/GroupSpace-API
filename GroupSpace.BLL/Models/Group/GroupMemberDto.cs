using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models.Group
{
    public class GroupMemberDto
    {
        public int GroupMemberId { get; set; }
        public DateTime JoinDate { get; set; }
        public int RoleTypeGroupId { get; set; }
        public UserDto User { get; set; }
    }
}
