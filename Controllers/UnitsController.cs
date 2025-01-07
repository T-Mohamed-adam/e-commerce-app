using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Unit;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/units
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UnitsController : Controller
    {
    
            private readonly IUnitService _unitService;

            public UnitsController(IUnitService unitService)
            {
                _unitService = unitService;
            }

            // Get all units
            [HttpGet]
            public async Task<IActionResult> GetAllUnites()
            {
                var units = await _unitService.GetAllUnites();
                return Ok(units);
            }

            // Get specific unit by its id
            [HttpGet("{id:int}")]
            public async Task<IActionResult> GetUnitById(int id)
            {
                var unit = await _unitService.GetUnitById(id);

                if (unit is null)
                {
                    return NotFound("Unit not found");
                }
                return Ok(unit);
            }

            // Add new unit
            [HttpPost]
            public async Task<IActionResult> AddUnit([FromForm] UnitAddRequest unitAddRequest)
            {
                try
                {
                    var unit = await _unitService.AddUnit(unitAddRequest);
                    return Ok(unit);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            // Update specific unit by its id
            [HttpPut("{id:int}")]
            public async Task<IActionResult> UpdateUnit(int id, [FromForm] UnitUpdateRequest unitUpdateRequest)
            {
                try
                {
                    var unit = await _unitService.UpdateUnit(id, unitUpdateRequest);

                    if (unit is null)
                    {
                        return NotFound("Unit not found");
                    }

                    return Ok(unit);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            // Delete specific unit by its id
            [HttpDelete("{id:int}")]
            public async Task<IActionResult> DeleteUnit(int id)
            {
                var unit = await _unitService.DeleteUnit(id);

                if (unit is null)
                {
                    return NotFound("Unit not found");
                }
                return Ok("Unit data deleted successfully");
            }
    }
}  
