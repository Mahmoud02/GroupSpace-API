using GroupSpace.BLL;
using GroupSpace.BLL.Models.Group;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GroupSpaceWeb.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GroupTypesController : ControllerBase
    {
        private readonly IGroupTypeService groupTypeService;
        public GroupTypesController(IGroupTypeService groupTypeService)
        {

            this.groupTypeService = groupTypeService;
        }

        // GET: api/<GroupTypeController>
        [HttpGet]
        public IActionResult Get()
        {
            var data = groupTypeService.All(); 
            return Ok( new { data });
        }

        // GET api/<GroupTypeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GroupTypeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GroupTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GroupTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
