using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Brand;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/brands
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService) 
        {
            _brandService = brandService;
        }

        // Get all brands
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrands();
            return Ok(brands);
        }

        // Get specific brand by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBrandById(int id) 
        {
            var brand = await _brandService.GetBrandById(id);

            if (brand is null) 
            {
                return NotFound("Brand not found");
            }
            return Ok(brand);
        }

        // Add new brand
        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandAddRequest brandAddRequest) 
        {
            try
            {
                var brand = await _brandService.AddBrand(brandAddRequest);
                return Ok(brand);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific brand by its id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBrand(int id, BrandUpdateRequest brandUpdateRequest)
        {
            try
            {
                var brand = await _brandService.UpdateBrand(id, brandUpdateRequest);

                if (brand is null)
                {
                    return NotFound("Brand not found");
                }

                return Ok(brand);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete specific brand by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandService.DeleteBrand(id);

            if (brand is null)
            {
                return NotFound("Brand not found");
            }
            return Ok("Brand data deleted successfully");
        }
    }
}
