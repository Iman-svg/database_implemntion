using Microsoft.AspNetCore.Mvc;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Repositories;

namespace LogisticsWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndustryGroupController : ControllerBase
    {
        private IIndustryGroupRepository _repo = new IndustryGroupRepository();

        [HttpPost("add")]
        public IActionResult AddIndustryGroup([FromBody] IndustryGroup group)
        {
            if (_repo.AddIndustryGroup(group))
                return Ok("Industry group added successfully");
            return BadRequest("Failed to add industry group");
        }

        [HttpGet("all")]
        public IActionResult GetAllIndustryGroups()
        {
            var groups = _repo.GetAllIndustryGroups();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public IActionResult GetIndustryGroupById(int id)
        {
            var group = _repo.GetIndustryGroupById(id);
            if (group == null)
                return NotFound("Industry group not found");
            return Ok(group);
        }

        [HttpPut("update")]
        public IActionResult UpdateIndustryGroup([FromBody] IndustryGroup group)
        {
            if (_repo.UpdateIndustryGroup(group))
                return Ok("Industry group updated successfully");
            return BadRequest("Failed to update industry group");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteIndustryGroup(int id)
        {
            if (_repo.DeleteIndustryGroup(id))
                return Ok("Industry group deleted successfully");
            return BadRequest("Failed to delete industry group");
        }
    }
}