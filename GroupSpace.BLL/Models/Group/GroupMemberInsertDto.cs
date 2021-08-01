using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models.Group
{
    public class GroupMemberInsertDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }
        [Required]
        public int RoleTypeGroupId { get; set; }
       
    }
}
