using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupTypeId { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Name { get; set; }
        [Required]
        public bool Private { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Column(TypeName = "text")]
        public string CoverPhotoUrl { get; set; }
        //Navigation
        public List<Post> Posts { get; set; }


    }
}
