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
    public class PostReportsController : ControllerBase
    {
        private readonly IReportPostService reportPostService;


        public PostReportsController(IReportPostService reportPostService)
        {
            this.reportPostService = reportPostService;
        }
        [HttpPost]
        public IActionResult Post([FromBody] ReportPostInsertDto report)
        {
            var response = reportPostService.Add(report);
            if (response.OpertaionState)
            {
                return Created("comments", new { Message = "Post is Reported Successfully" });
            }
            else
            {
                return StatusCode(500, new { Message = response.Message });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!reportPostService.CheckIfReportPostExist(id)) return NotFound(new { Message = "There is no group with such Id" });
            var response = reportPostService.Delete(id);
            if (response.OpertaionState) return Ok(new { Message = "Comment Is Deleted Successfully" });
            else
            {
                return StatusCode(500, response.Message);
            }
        }
    }
}
