using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Store.Application.DTOs;
using Store.Application.Services;
using Store.Core.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService= productService;
            _logger = logger;
        }
        [HttpGet("Get All products")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductDto>>GetBYid(int id)
        {
            var product=await  _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogError($"there is no such id {id}");
                return BadRequest($"there is no such id= {id}");
            }
            
            return Ok(product);
        }
        [HttpPost("Add Product")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>>CreateProducts(CreateProductDto createProduct)
        {
           var product=await _productService.CreateProductAsync(createProduct);
            if (product.Price <= 0)
            {
                return BadRequest(new { message = "Price must be greater than 0" });
            }
            if (!ModelState.IsValid)
                 return BadRequest(new
                {
                    message = "There is non valid data"
                });
            return Ok(product);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ✅ AWAIT the async method
               await _productService.UpdateProductAsync(id, updateProductDto);
               return NoContent();
        }
        [HttpDelete("Deactive Product/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult>DeleteProduct(int id)
        {
           var product= await _productService.DeleteProductAsync(id);
            if (!ModelState.IsValid)
            {
                _logger.LogError($"there is no such id {id}");
                return BadRequest(ModelState);
            }
            if(product==null)
                _logger.LogError($"there is no such id {id}");
            return NoContent();
        }
        [HttpGet("Search Product/{term}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductDto>>>SearchProducts(string term)
        {
            var product=await _productService.SearchProductAsync(term);
            return Ok(product);
        }
    }
}

