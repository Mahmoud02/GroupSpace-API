using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Entities
{
    public class RoleTypeGroup
    {
        public int RoleTypeGroupId { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Text { get; set; }

       

    }
}
