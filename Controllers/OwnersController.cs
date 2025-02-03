using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Owner;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/owners
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OwnersController : Controller
    {
        private readonly IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService) 
        {
            _ownerService = ownerService;
        }

        // Get all owners
        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            var owners = await _ownerService.GetAllOwners();
            return Ok(owners);
        }

        // Get specific owner by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOwnerById(int  id) 
        {
            var owner = await _ownerService.GetOwnerById(id);
            if (owner is null) 
            {
                return NotFound("Owner not found");
            }
            return Ok(owner);
        }

        // Add new owner
        [HttpPost]
        public async Task<IActionResult> AddOwner(OwnerAddRequest ownerAddRequest) 
        {
            try
            {
                var owner = await _ownerService.AddOwner(ownerAddRequest);
                return Ok(owner);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
           
        }

        // Update specific owner data 
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOwner(int id, OwnerUpdateRequest ownerUpdateRequest) 
        {

                var owner = await _ownerService.UpdateOwner(id, ownerUpdateRequest);

                if (owner is null)
                {
                    return NotFound("Owner not found");
                }

                return Ok(owner);
            
        }

        // Delete specific owner data 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOwner(int id) 
        {
            var owner = await _ownerService.DeleteOwner(id);

            if (owner is null)
            {
                return NotFound("Owner not found");
            }
            return Ok("Owner data deleted successfully");
        }
    }
}
