using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models
{
    public class PostInsertDto
    {
        public int PostId { get; set; }
        public String Text { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public int NumOfLikes { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime Date { get; set; }
    }
}
