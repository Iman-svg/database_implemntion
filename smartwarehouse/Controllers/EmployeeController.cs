using Microsoft.AspNetCore.Mvc;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Repositories;

namespace LogisticsWarehouse.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EmployeeController : ControllerBase
	{
		private IEmployeeRepository _repo = new EmployeeRepository();

		[HttpPost("add")]
		public IActionResult AddEmployee([FromBody] Employee employee)
		{
			if (_repo.AddEmployee(employee))
				return Ok("Employee added successfully");
			return BadRequest("Failed to add employee");
		}

		[HttpGet("all")]
		public IActionResult GetAllEmployees()
		{
			var employees = _repo.GetAllEmployees();
			return Ok(employees);
		}

		[HttpGet("{id}")]
		public IActionResult GetEmployeeById(int id)
		{
			var employee = _repo.GetEmployeeById(id);
			if (employee == null)
				return NotFound("Employee not found");
			return Ok(employee);
		}

		[HttpPut("update")]
		public IActionResult UpdateEmployee([FromBody] Employee employee)
		{
			if (_repo.UpdateEmployee(employee))
				return Ok("Employee updated successfully");
			return BadRequest("Failed to update employee");
		}

		[HttpDelete("delete/{id}")]
		public IActionResult DeleteEmployee(int id)
		{
			if (_repo.DeleteEmployee(id))
				return Ok("Employee deleted successfully");
			return BadRequest("Failed to delete employee");
		}
	}
}