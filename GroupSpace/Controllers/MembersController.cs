using GroupSpace.BLL;
using GroupSpace.BLL.Models.Group;
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
    [Authorize]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IGroupMemberService groupMemberService;
        public MembersController(IGroupMemberService groupMemberService)
        {
            this.groupMemberService = groupMemberService;
        }
        [HttpGet]
        public IEnumerable<GroupMemberDto> Get()
        {
            return groupMemberService.All();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!groupMemberService.CheckIfGroupMemberExist(id)) return NotFound(new { Message = "There is no Member with such Id" });

            return Ok(groupMemberService.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] GroupMemberInsertDto groupMember)
        {
            groupMember.JoinDate = DateTime.Now;
            var response = groupMemberService.Add(groupMember);
            if (response.OpertaionState)
            {
                return Created("request", new { Message = " GroupMember is Added Succesfully" });
            }
            else
            {
                return StatusCode(500, new { Message = response.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GroupMemberInsertDto groupMember)
        {
            if (!groupMemberService.CheckIfGroupMemberExist(id)) return NotFound(new { Message = "There is no GroupMember with such Id" });
            var response = groupMemberService.Update(groupMember);
            if (response.OpertaionState) return Ok(new { Message = "GroupMember Is Updated Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!groupMemberService.CheckIfGroupMemberExist(id)) return NotFound(new { Message = "There is no GroupMember with such Id" });
            var response = groupMemberService.Delete(id);
            if (response.OpertaionState) return Ok(new { Message = "GroupMember Is Deleted Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }
    }
}
