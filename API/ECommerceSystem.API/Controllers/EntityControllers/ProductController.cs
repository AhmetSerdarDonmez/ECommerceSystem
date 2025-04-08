using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceSystem.Application.Repositories;

namespace ECommerceSystem.API.Controllers.EntityControllers
{       
        [Route("api/[controller]")]
        [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductController(IProductReadRepository productReadRepository , IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet("get-all-product")]
        [Authorize]
        public IActionResult GetAllProducts()
        {
            if (User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value != "1")
            {
                return Unauthorized("You do not have the required role to access this resource.");
            }
            var products = _productReadRepository.GetAll().ToList();
            return Ok(products);
        }

        [HttpGet("get-product-by-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("add-single-product")]
        [Authorize]
        public async Task<IActionResult> AddSingleProduct([FromBody] Domain.Entities.Products.Product product)
        {
            var isAdded = await _productWriteRepository.AddAsync(product);
            if (!isAdded)
            {
                return BadRequest("Product could not be added");
            }
            var count = await _productWriteRepository.SaveAsync();
            return Ok(new { message = "Product added successfully", rowsAffected = count });
        }

        [HttpPost("add-range-product")]
        [Authorize]

        public async Task<IActionResult> AddRangeProduct([FromBody] List<Domain.Entities.Products.Product> products)
        {
            var isAdded = await _productWriteRepository.AddRangeAsync(products);
            if (!isAdded)
            {
                return BadRequest("Products could not be added");
            }
            var count = await _productWriteRepository.SaveAsync();
            return Ok(new { message = "Products added successfully", rowsAffected = count });
        }


        [HttpPut("update-product/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Domain.Entities.Products.Product product)
        {
            var existingProduct = await _productReadRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;
            existingProduct.ProductDescription = product.ProductDescription;
            var isUpdated = await _productWriteRepository.SaveAsync();
            var count = await _productWriteRepository.SaveAsync();
            return Ok(new { message = "Product updated successfully", rowsAffected = count });
        }

        [HttpDelete("remove-product-by-id/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _productWriteRepository.Remove(product);
            var count = await _productWriteRepository.SaveAsync();
            return Ok(new { message = "Product deleted successfully", rowsAffected = count });
        }

        [HttpDelete("remove-single-product")]
        [Authorize]
        public async Task<IActionResult> DeleteSingleProduct([FromBody] Domain.Entities.Products.Product product)
        {
            var isDeleted = _productWriteRepository.Remove(product);
            if (!isDeleted)
            {
                return BadRequest("Product could not be deleted");
            }
            var count = await _productWriteRepository.SaveAsync();
            return Ok(new { message = "Product deleted successfully", rowsAffected = count });
        }

        [HttpDelete("remove-range-product")]
        [Authorize]
        public async Task<IActionResult> DeleteRangeProduct([FromBody] List<Domain.Entities.Products.Product> products)
        {
            var isDeleted = _productWriteRepository.RemoveRange(products);
            if (!isDeleted)
            {
                return BadRequest("Products could not be deleted");
            }
            var count = await _productWriteRepository.SaveAsync();
            return Ok(new { message = "Products deleted successfully", rowsAffected = count });
        }




    }
}
