using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models
{
    public class GroupInsertDto
    {
        public int GroupId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Private { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile CoverPhoto { get; set; }
    }
}
