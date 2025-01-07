using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.City;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/cities
    [Route("api/v1/[controller]")]
    [ApiController]

    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // Get all cities
        [HttpGet]
        public async Task<IActionResult> GetAllCity()
        {
           var cities = await _cityService.GetAllCity();

            return Ok(cities);
        }

        // Get specific city by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCityById(int id) 
        {
            var city = await _cityService.GetCityById(id);
            if (city is null) 
            {
                return NotFound("City not found");
            }
            return Ok(city);
        }

        // Add new city 
        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] CityAddRequest cityAddRequest)
        {
            try
            {
                var city = await _cityService.AddCity(cityAddRequest);
                return Ok(city);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // Update specific city data
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateRequest cityUpdateRequest) 
        {
            try
            {
                var city = await _cityService.UpdateCity(id, cityUpdateRequest);

                if (city is null)
                {
                    return NotFound("City not found");
                }

                return Ok(city);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete specific city data
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCity(int id) 
        {
            var city = await _cityService.DeleteCity(id);

            if (city is null)
            {
                return NotFound("City not found");
            }
            return Ok("City deleted Successfully");
        }
    }
}
