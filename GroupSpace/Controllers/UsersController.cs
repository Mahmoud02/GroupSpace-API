using GroupSpace.BLL;
using GroupSpace.BLL.Models;
using GroupSpace.BLL.Models.User;
using GroupSpace.BLL.Shared;
using GroupSpaceWeb.Helpers;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroupSpaceWeb.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IGroupService groupService;
        private readonly IJoinRequestService joinRequestService;
        private readonly IGroupMemberService groupMemberService;
        private readonly IAuthorizationService authorizationService;

        public UsersController(
            IUserService userService,
            IOptions<JwtSettings> jwtSettings,
            IGroupService groupService,
            IJoinRequestService joinRequestService,
            IGroupMemberService groupMemberService,
            IAuthorizationService authorizationService
            )
        {
            this.userService = userService;
            this.groupService = groupService;
            this.joinRequestService = joinRequestService;
            this.groupMemberService = groupMemberService;
            this.authorizationService = authorizationService;

        }

        // GET: api/<UserController>

        [HttpGet]
        public IEnumerable<UserDto> Get()
        
        {         
            return userService.All();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public  IActionResult  Get(int id)
        {           
            if (!userService.CheckIfUserExist(id)) return NotFound(new { Message = "There is no user with such Id" });
            return Ok(userService.Get(id)); 
        }
        // POST api/<UserController>

        [HttpPost]
        [Authorize(AuthenticationSchemes = "IdpServerSchema")]
        public IActionResult Post()
        {
            var userSubID = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var userName = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;


            UserInsertDto userInsert = new() {
                UserName = userName,
                SubID = userSubID,
                PersonalImageUrl = "https://e7.pngegg.com/pngimages/348/800/png-clipart-man-wearing-blue-shirt-illustration-computer-icons-avatar-user-login-avatar-blue-child.png"
            };

            try
            {
                // create user
                var response = userService.Add(userInsert);
                if (response.OpertaionState) return Ok(new { message = "User is created successfully" });
                else
                {
                    //unexpected Errors happens in server
                    return StatusCode(500);
                }
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
        #region Get user realted resource

        [Authorize]
        [HttpGet("{sub}/groups")]
        public IActionResult GetUserGroups(string sub)
        {
            var data = groupService.GetUserGroups(sub);
            return Ok(new { data });
        }
        [HttpGet("{sub}/joined")]
        public IActionResult GetUserJoinedGroups(string sub)
        {
            //get User Sub
            var data = groupMemberService.GetUserJoinedGroups(sub);
            return Ok(new { data });
        }
        [HttpGet("{sub}/requests")]
        public IActionResult GetGroupsIdOfJoinRequestByUserId(string sub)
        {
            var data = joinRequestService.GetGroupsIdOfJoinRequestByUserId(sub);
            return Ok(new { data });
        }
        #endregion
    }
}
/*code for test*/
/*
 var roles = User.Identity;
            var val = User.IsInRole("Admin");
            var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var idt = Int16.Parse(ownerId);
            var result = await this.authorizationService.AuthorizeAsync(User, "MarketingTeam");

 
 */