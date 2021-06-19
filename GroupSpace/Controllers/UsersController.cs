using GroupSpace.BLL;
using GroupSpace.BLL.Models;
using GroupSpace.BLL.Models.User;
using GroupSpace.BLL.Shared;
using GroupSpaceWeb.Helpers;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly JwtSettings jwtSettings;


        public UsersController(IUserService userService, IOptions<JwtSettings> jwtSettings)
        {
            this.userService = userService;
            this.jwtSettings = jwtSettings.Value;

        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<UserDto> Get()
        
        {
            return userService.All();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!userService.CheckIfUserExist(id)) return NotFound(new { Message = "There is no user with such Id" });

            return Ok(userService.Get(id)); 
        }

        // POST api/<UserController>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(UserInsertDto userInsert )
        {
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

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserInsertDto userInsert)
        {

            if (!userService.CheckIfUserExist(id)) return NotFound(new { Message = "There is no user with such Id" });
            var response = userService.Update(userInsert);
            if (response.OpertaionState) return Ok(new { Message = "User Is Updated Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            if (!userService.CheckIfUserExist(id)) return NotFound(new { Message = "There is no user with such Id" });
            var response = userService.Delete(id);
            if (response.OpertaionState) return Ok(new { Message = "User Is Deleted Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }


        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            var claims = new List<Claim>();
            claims.Add(new Claim("UserId", user.UserId.ToString()));
            
            var tokenDescriptor = JwtHelper.GetJwtToken(
                        user.UserName,
                        jwtSettings.SigningKey,
                        jwtSettings.Issuer,
                        jwtSettings.Audience,
                        TimeSpan.FromDays(jwtSettings.TokenTimeoutDays),
                        claims.ToArray());
            var token  = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            // return basic user info and authentication token
            return Ok(new
            {
                user.UserId,
                user.UserName,
                user.Email,
                user.PersonalImageUrl,
                Token = token
            });
        }
    }
}
