using Microsoft.AspNetCore.Mvc;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Repositories;
using System.Collections.Generic;

namespace LogisticsWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacilityController : ControllerBase
    {
        private IFacilityRepository _repo = new FacilityRepository();

        [HttpPost("add")]
        public IActionResult AddFacility([FromBody] Facility facility)
        {
            if (_repo.AddFacility(facility))
                return Ok("Facility added successfully");
            return BadRequest("Failed to add facility");
        }

        [HttpGet("all")]
        public IActionResult GetAllFacilities()
        {
            var facilities = _repo.GetAllFacilities();
            return Ok(facilities);
        }

        [HttpGet("{id}")]
        public IActionResult GetFacilityById(int id)
        {
            var facility = _repo.GetFacilityById(id);
            if (facility == null)
                return NotFound("Facility not found");
            return Ok(facility);
        }

        [HttpPut("update")]
        public IActionResult UpdateFacility([FromBody] Facility facility)
        {
            if (_repo.UpdateFacility(facility))
                return Ok("Facility updated successfully");
            return BadRequest("Failed to update facility");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteFacility(int id)
        {
            if (_repo.DeleteFacility(id))
                return Ok("Facility deleted successfully");
            return BadRequest("Failed to delete facility");
        }
    }
}