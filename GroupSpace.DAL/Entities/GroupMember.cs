using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Entities
{
    public class GroupMember
    {
        public int GroupMemberId { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }
        [Required]
        public int RoleTypeGroupId { get; set; }

        //Navigation
        public User User { get; set; }
        public Group Group { get; set; }
        public GroupRoleType RoleType { get; set; }
    }
}
