using Microsoft.AspNetCore.Mvc;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Repositories;

namespace LogisticsWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageAgreementController : ControllerBase
    {
        private IStorageAgreementRepository _repo = new StorageAgreementRepository();

        [HttpPost("add")]
        public IActionResult AddStorageAgreement([FromBody] StorageAgreement agreement)
        {
            if (_repo.AddStorageAgreement(agreement))
                return Ok("Storage agreement added successfully");
            return BadRequest("Failed to add storage agreement");
        }

        [HttpGet("all")]
        public IActionResult GetAllStorageAgreements()
        {
            var agreements = _repo.GetAllStorageAgreements();
            return Ok(agreements);
        }

        [HttpGet("{id}")]
        public IActionResult GetStorageAgreementById(int id)
        {
            var agreement = _repo.GetStorageAgreementById(id);
            if (agreement == null)
                return NotFound("Storage agreement not found");
            return Ok(agreement);
        }

        [HttpGet("client/{clientId}")]
        public IActionResult GetStorageAgreementsByClient(int clientId)
        {
            var agreements = _repo.GetStorageAgreementsByClient(clientId);
            return Ok(agreements);
        }

        [HttpPut("update")]
        public IActionResult UpdateStorageAgreement([FromBody] StorageAgreement agreement)
        {
            if (_repo.UpdateStorageAgreement(agreement))
                return Ok("Storage agreement updated successfully");
            return BadRequest("Failed to update storage agreement");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteStorageAgreement(int id)
        {
            if (_repo.DeleteStorageAgreement(id))
                return Ok("Storage agreement deleted successfully");
            return BadRequest("Failed to delete storage agreement");
        }
    }
}