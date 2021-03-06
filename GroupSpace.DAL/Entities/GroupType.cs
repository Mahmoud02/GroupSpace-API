using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Entities
{
    public class GroupType
    {
        public int GroupTypeId { get; set; }
        
        [Column(TypeName = "text")]
        public string Text { get; set; }

        //Navigation
        public List<Group> Groups { get; set; }

    }
}
