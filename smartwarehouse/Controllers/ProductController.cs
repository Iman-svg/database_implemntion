using Microsoft.AspNetCore.Mvc;
using LogisticsWarehouse.Models;
using LogisticsWarehouse.Repositories;
using System.Collections.Generic;

namespace LogisticsWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repo = new ProductRepository();

        [HttpPost("add")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (_repo.AddProduct(product))
                return Ok("Product added successfully");
            return BadRequest("Failed to add product");
        }

        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            var products = _repo.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
                return NotFound("Product not found");
            return Ok(product);
        }

        [HttpGet("manufacturer/{manufacturerId}")]
        public IActionResult GetProductsByManufacturer(int manufacturerId)
        {
            var products = _repo.GetProductsByManufacturer(manufacturerId);
            return Ok(products);
        }

        [HttpGet("industry/{industryGroupId}")]
        public IActionResult GetProductsByIndustryGroup(int industryGroupId)
        {
            var products = _repo.GetProductsByIndustryGroup(industryGroupId);
            return Ok(products);
        }

        [HttpPut("update")]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            if (_repo.UpdateProduct(product))
                return Ok("Product updated successfully");
            return BadRequest("Failed to update product");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (_repo.DeleteProduct(id))
                return Ok("Product deleted successfully");
            return BadRequest("Failed to delete product");
        }
    }
}