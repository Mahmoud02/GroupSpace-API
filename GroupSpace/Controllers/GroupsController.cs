using GroupSpace.BLL;
using GroupSpace.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroupSpaceWeb.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IPostService postService;
        private readonly IReportPostService reportPostService;

        private readonly IGroupMemberService groupMemberService;


        public GroupsController(IGroupService groupService,IPostService postService, IGroupMemberService groupMemberService, IReportPostService reportPostService)
        {
            this.groupService = groupService;
            this.postService = postService;
            this.groupMemberService = groupMemberService;
            this.reportPostService = reportPostService;

        }
        // GET: api/<GroupController>
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IEnumerable<GroupDto> Get()
        {
            return groupService.All();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            if (!groupService.CheckIfGroupExist(id)) return NotFound(new { Message = "There is no group with such Id" });

            return Ok(groupService.Get(id));
        }
       

        [HttpPost]
        public IActionResult Post([FromForm] GroupInsertDto group)
        {
            //get User Sub
            var sub = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            var response = groupService.Add( sub, group).Result;
            if (response.OpertaionState) {
                return Created("group", new { Message ="Group is Added Succesfully" });
            } 
            else
            {
                return StatusCode(500, new { Message = response.Message });
            }
        }

        [HttpPut("{Id}")]
        [Authorize(Policy = "MustOwnGroup")]
        public IActionResult Put(int Id, [FromBody] GroupInsertDto group)
        {
            if (!groupService.CheckIfGroupExist(Id)) return NotFound(new { Message = "There is no group with such Id" });
            var response = groupService.Update(group);
            if (response.OpertaionState) return Ok(new { Message = "Group Is Updated Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }

        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "MustOwnGroup")]
        public IActionResult Delete(int Id)
        {
            if (!groupService.CheckIfGroupExist(Id)) return NotFound(new { Message = "There is no group with such Id" });
            var response = groupService.Delete(Id);
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
        [Authorize(Policy = "MustOwnGroup")]
        public IActionResult Requests(int Id)
        {
            var data = groupService.GetJoinRequests(Id);
            return Ok(new { data });
        }
        [HttpGet("{Id}/reports")]
        [Authorize(Policy = "MustOwnGroup")]
        public IActionResult Reports(int Id)
        {
            var data = reportPostService.GetReportedPosts(Id);
            return Ok(new { data });
        }
        #endregion

        #region Find Groups For The User
        [HttpGet("Find")]
        public IActionResult Find()
        {
            var sub = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var data = groupService.FindGroups(sub);
            return Ok(new { data });
        }
        #endregion
    }
}
