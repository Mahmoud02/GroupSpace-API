using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models.Chat
{
    class ChatInsertDto
    {
        [Required]
        public string Sub { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
