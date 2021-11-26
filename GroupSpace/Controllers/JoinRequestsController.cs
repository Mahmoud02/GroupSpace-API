using GroupSpace.BLL;
using GroupSpace.BLL.Models.Group;
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
    public class JoinRequestsController : ControllerBase
    {
        private readonly IJoinRequestService joinRequestService;
        private readonly IGroupMemberService groupMemberService;


        public JoinRequestsController(IJoinRequestService joinRequestService, IGroupMemberService groupMemberService)
        {
            this.joinRequestService = joinRequestService;
            this.groupMemberService = groupMemberService;
        }
        [HttpGet]
        public IEnumerable<JoinRequestDto> Get()
        {
            return joinRequestService.All();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!joinRequestService.CheckIfRequestExist(id)) return NotFound(new { Message = "There is no Request with such Id" });

            return Ok(joinRequestService.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] JoinRequestInsertDto joinRequest)
        {
            var sub = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            joinRequest.Date = DateTime.Now;
            var response = joinRequestService.Add(sub, joinRequest);
            if (response.OpertaionState)
            {
                return Created("request", new { Message = "joinRequest is Added Succesfully" });
            }
            else
            {
                return StatusCode(500, new { Message = response.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!joinRequestService.CheckIfRequestExist(id)) return NotFound(new { Message = "There is no Request with such Id" });
            var response = joinRequestService.Delete(id);
            if (response.OpertaionState) return Ok(new { Message = "Request Is Deleted Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(JoinRequestInsertDto joinRequest)
        {
            var sub = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            var id = joinRequestService.GetJoinRequestID(sub, joinRequest.GroupId);
            if(id ==0) return BadRequest(new { Message = "This user didnt join request for that group" });

            var response = joinRequestService.Delete(id);
            if (response.OpertaionState) return Ok(new { Message = "JoinRequest Is Deleted Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }

        [HttpPost("{Id}/Accept")]
        public IActionResult Accept(int Id)
        {
            var joinRequest = joinRequestService.Get(Id);

            GroupMemberInsertDto groupMember = new()
            {
                UserId = joinRequest.UserId,
                GroupId = joinRequest.GroupId,
                JoinDate = DateTime.Now,
                RoleTypeGroupId =1

            };
            var response = groupMemberService.Add(groupMember);
            if (response.OpertaionState)
            {
                var RESULT = joinRequestService.Delete(Id);
                if (response.OpertaionState) return Ok(new { Message = "User is Added Suceesfully" });
                else
                {
                    return StatusCode(500, response.Message);
                }
            }
            else
            {
                return StatusCode(500, new { Message = response.Message });
            }

        }

        #region RelatedObject

        #endregion
    }
}
