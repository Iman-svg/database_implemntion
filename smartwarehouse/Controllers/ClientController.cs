using Microsoft.AspNetCore.Mvc;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Repositories;

namespace LogisticsWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private IClientRepository _repo = new ClientRepository();

        [HttpPost("add")]
        public IActionResult AddClient([FromBody] Client client)
        {
            if (_repo.AddClient(client))
                return Ok("Client added successfully");
            return BadRequest("Failed to add client");
        }

        [HttpGet("all")]
        public IActionResult GetAllClients()
        {
            var clients = _repo.GetAllClients();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = _repo.GetClientById(id);
            if (client == null)
                return NotFound("Client not found");
            return Ok(client);
        }

        [HttpPut("update")]
        public IActionResult UpdateClient([FromBody] Client client)
        {
            if (_repo.UpdateClient(client))
                return Ok("Client updated successfully");
            return BadRequest("Failed to update client");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteClient(int id)
        {
            if (_repo.DeleteClient(id))
                return Ok("Client deleted successfully");
            return BadRequest("Failed to delete client");
        }
    }
}