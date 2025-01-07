using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Category;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/categories
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
        }
        // Get all categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        // Get specific category by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category is null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        // Add new category
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddRequest categoryAddRequest)
        {
            try
            {
                var category = await _categoryService.AddCategory(categoryAddRequest);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific category by its id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateRequest categoryUpdateRequest)
        {
            try
            {
                var category = await _categoryService.UpdateCategory(id, categoryUpdateRequest);

                if (category is null)
                {
                    return NotFound("Category not found");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete specific category by its id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.DeleteCategory(id);

            if (category is null)
            {
                return NotFound("Category not found");
            }
            return Ok("Category data deleted successfully");
        }
    }
}
