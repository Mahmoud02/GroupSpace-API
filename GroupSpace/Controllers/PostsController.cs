using GroupSpace.BLL;
using GroupSpace.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroupSpaceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService postService;
        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }
        // GET: api/<PostsController>
        [HttpGet]
        public IEnumerable<PostDto> Get()
        {
            return postService.All();

        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!postService.CheckIfPostExist(id)) return NotFound(new { Message = "There is no post with such Id" });

            return Ok(postService.Get(id));
        }

        // POST api/<PostsController>
        [HttpPost]
        public IActionResult Post([FromForm] PostInsertDto post)
        {
            post.Date = DateTime.Now;
            var response = postService.Add(post).Result;
            if (response.OpertaionState)
            {
                return Created("group", new { Message = "Post is Published Succesfully" });
            }
            else
            {
                return StatusCode(500, new { Message = response.Message });
            }

        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PostInsertDto post)
        {
            if (!postService.CheckIfPostExist(id)) return NotFound(new { Message = "There is no post with such Id" });
            var response = postService.Update(post);
            if (response.OpertaionState) return Ok(new { Message = "Post Is Updated Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!postService.CheckIfPostExist(id)) return NotFound(new { Message = "There is no post with such Id" });
            var response = postService.Delete(id);
            if (response.OpertaionState) return Ok(new { Message = "post Is Deleted Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }
       
    }
}
