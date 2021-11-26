using GroupSpace.BLL;
using GroupSpace.BLL.Models.PostComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace GroupSpaceWeb.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PostCommentsController : ControllerBase
    {
        private readonly IPostCommentService postCommentService;


        public PostCommentsController(IPostCommentService postCommentService)
        {
            this.postCommentService = postCommentService;
        }
        [HttpPost]
        public IActionResult Post([FromBody] PostCommentDto commentDto)
        {
            //get User Sub
            var sub = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            commentDto.Date = DateTime.Now;
            var response = postCommentService.Add(sub, commentDto);
            if (response.OpertaionState)
            {
                return Created("comments", new { Message = "Comment is Added Succesfully" });
            }
            else
            {
                return StatusCode(500, new { Message = response.Message });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!postCommentService.CheckIfCommentExist(id)) return NotFound(new { Message = "There is no group with such Id" });
            var response = postCommentService.Delete(id);
            if (response.OpertaionState) return Ok(new { Message = "Comment Is Deleted Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }
    }
}
