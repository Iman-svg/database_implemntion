using Microsoft.AspNetCore.Mvc;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Repositories;

namespace LogisticsWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private IManufacturerRepository _repo = new ManufacturerRepository();

        [HttpPost("add")]
        public IActionResult AddManufacturer([FromBody] Manufacturer manufacturer)
        {
            if (_repo.AddManufacturer(manufacturer))
                return Ok("Manufacturer added successfully");
            return BadRequest("Failed to add manufacturer");
        }

        [HttpGet("all")]
        public IActionResult GetAllManufacturers()
        {
            var manufacturers = _repo.GetAllManufacturers();
            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public IActionResult GetManufacturerById(int id)
        {
            var manufacturer = _repo.GetManufacturerById(id);
            if (manufacturer == null)
                return NotFound("Manufacturer not found");
            return Ok(manufacturer);
        }

        [HttpPut("update")]
        public IActionResult UpdateManufacturer([FromBody] Manufacturer manufacturer)
        {
            if (_repo.UpdateManufacturer(manufacturer))
                return Ok("Manufacturer updated successfully");
            return BadRequest("Failed to update manufacturer");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteManufacturer(int id)
        {
            if (_repo.DeleteManufacturer(id))
                return Ok("Manufacturer deleted successfully");
            return BadRequest("Failed to delete manufacturer");
        }
    }
}