using Microsoft.AspNetCore.Mvc;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Repositories;
using System;
using System.Collections.Generic;

namespace LogisticsWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementController : ControllerBase
    {
        private IMovementRepository _repo = new MovementRepository();

        [HttpPost("add")]
        public IActionResult AddMovement([FromBody] Movement movement)
        {
            movement.Timestamp = DateTime.Now; // Auto-set timestamp
            if (_repo.AddMovement(movement))
                return Ok("Movement recorded successfully");
            return BadRequest("Failed to record movement");
        }

        [HttpGet("all")]
        public IActionResult GetAllMovements()
        {
            var movements = _repo.GetAllMovements();
            return Ok(movements);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovementById(int id)
        {
            var movement = _repo.GetMovementById(id);
            if (movement == null)
                return NotFound("Movement not found");
            return Ok(movement);
        }

        [HttpGet("employee/{employeeId}")]
        public IActionResult GetMovementsByEmployee(int employeeId)
        {
            var movements = _repo.GetMovementsByEmployee(employeeId);
            return Ok(movements);
        }

        [HttpGet("product/{productId}")]
        public IActionResult GetMovementsByProduct(int productId)
        {
            var movements = _repo.GetMovementsByProduct(productId);
            return Ok(movements);
        }

        [HttpGet("facility/{facilityId}")]
        public IActionResult GetMovementsByFacility(int facilityId)
        {
            var movements = _repo.GetMovementsByFacility(facilityId);
            return Ok(movements);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteMovement(int id)
        {
            if (_repo.DeleteMovement(id))
                return Ok("Movement deleted successfully");
            return BadRequest("Failed to delete movement");
        }
    }
}