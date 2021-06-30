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
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IPostService postService;

        public GroupsController(IGroupService groupService,IPostService postService)
        {
            this.groupService = groupService;
            this.postService = postService;
        }
        // GET: api/<GroupController>
        [HttpGet]
        public IEnumerable<GroupDto> Get()
        {
            return groupService.All();
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!groupService.CheckIfGroupExist(id)) return NotFound(new { Message = "There is no group with such Id" });

            return Ok(groupService.Get(id));
        }
       

        // POST api/<GroupController>
        [HttpPost]
        public IActionResult Post([FromForm] GroupInsertDto group)
        {
            var response = groupService.Add(group).Result;
            if (response.OpertaionState) {
                return Created("group", new { Message ="Group is Added Succesfully" });
            } 
            else
            {
                return StatusCode(500, new { Message = response.Message });
            }
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GroupInsertDto group)
        {
            if (!groupService.CheckIfGroupExist(id)) return NotFound(new { Message = "There is no group with such Id" });
            var response = groupService.Update(group);
            if (response.OpertaionState) return Ok(new { Message = "Group Is Updated Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }

        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!groupService.CheckIfGroupExist(id)) return NotFound(new { Message = "There is no group with such Id" });
            var response = groupService.Delete(id);
            if (response.OpertaionState) return Ok(new { Message = "Group Is Deleted Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }

        #region Get group realted resource
       
        [HttpGet("{Id}/posts")]
        public IActionResult GroupPosts(int Id)
        {
            var data = postService.GroupPosts(Id);
            return Ok(new { data });
        }
        [HttpGet("{Id}/meta")]
        public IActionResult GroupMetaData(int Id)
        {
            var data = groupService.GetGroupMetaData(Id);
            return Ok(data);
        }
        [HttpGet("{Id}/members")]
        public IActionResult Members(int Id)
        {
            var data = groupService.GetGroupUsers(Id);
            return Ok(new { data });
        }
        [HttpGet("{Id}/requests")]
        public IActionResult Requests(int Id)
        {
            var data = groupService.GetJoinRequests(Id);
            return Ok(new { data });
        }
        [HttpGet("{Id}/reports")]
        public IActionResult Reports(int Id)
        {
            var data = groupService.GetReportedPosts(Id);
            return Ok(new { data });
        }
        #endregion
    }
}
