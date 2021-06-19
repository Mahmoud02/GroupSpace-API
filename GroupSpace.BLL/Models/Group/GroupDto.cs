using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int GroupTypeId { get; set; }
        public string Name { get; set; }
        public bool Private { get; set; }
        public string Description { get; set; }
        public string CoverPhotoUrl { get; set; }
    }
}
