using Microsoft.AspNetCore.Mvc;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Repositories;

namespace LogisticsWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : ControllerBase
    {
        private ISectionRepository _repo = new SectionRepository();

        [HttpPost("add")]
        public IActionResult AddSection([FromBody] Section section)
        {
            if (_repo.AddSection(section))
                return Ok("Section added successfully");
            return BadRequest("Failed to add section");
        }

        [HttpGet("all")]
        public IActionResult GetAllSections()
        {
            var sections = _repo.GetAllSections();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public IActionResult GetSectionById(int id)
        {
            var section = _repo.GetSectionById(id);
            if (section == null)
                return NotFound("Section not found");
            return Ok(section);
        }

        [HttpGet("facility/{facilityId}")]
        public IActionResult GetSectionsByFacility(int facilityId)
        {
            var sections = _repo.GetSectionsByFacility(facilityId);
            return Ok(sections);
        }

        [HttpPut("update")]
        public IActionResult UpdateSection([FromBody] Section section)
        {
            if (_repo.UpdateSection(section))
                return Ok("Section updated successfully");
            return BadRequest("Failed to update section");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteSection(int id)
        {
            if (_repo.DeleteSection(id))
                return Ok("Section deleted successfully");
            return BadRequest("Failed to delete section");
        }
    }
}