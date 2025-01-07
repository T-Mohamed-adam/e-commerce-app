using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Product;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/products
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Get all products 
        [HttpGet]
        public async Task<IActionResult> GetAllProducts() 
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        // Get specific product data by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id) 
        {
            var product = await _productService.GetProductById(id);

            if (product is null) 
            {
                return NotFound("Product not found");
            }

            return Ok(product);
        }

        // Add new product
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddRequest productAddRequest) 
        {
            try
            {
                var product = await _productService.AddProduct(productAddRequest);
                return Ok(product);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific product data 
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateRequest productUpdateRequest) 
        {
            try
            {
                var product = await _productService.UpdateProduct(id, productUpdateRequest);

                if (product is null) 
                {
                    return NotFound("Product not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete specific product data 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id) 
        {
            var product = await _productService.DeleteProduct(id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            return Ok("Product data deleted successfully");
        }
    }
}
