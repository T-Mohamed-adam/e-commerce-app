using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Package;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/packages
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PackagesController : Controller
    {
        private readonly IPackageService _packageService;

        public PackagesController(IPackageService packageService) 
        {
            _packageService = packageService;   
        }

        // Get all packages
        [HttpGet]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await _packageService.GetAllPackages();
            return Ok(packages);
        }

        // Get specific package by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPackageById(int id) 
        {
            var package = await _packageService.GetPackageById(id);
            if (package is null) 
            {
                return NotFound("Package not found");
            }
            return Ok(package);
        }

        // Add new Package
        [HttpPost]
        public async Task<IActionResult> AddPackage(PackageAddRequest packageAddRequest) 
        {
            try
            {
                var package = await _packageService.AddPackage(packageAddRequest);
                return Ok(package);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
           
        }

        //Update specific package data
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePackage(int id, PackageUpdateRequest packageUpdateRequest) 
        {
            try
            {
                var package = await _packageService.UpdatePackage(id, packageUpdateRequest);
                if (package is null) 
                {
                    return NotFound("Package not found");
                }
                return Ok(package);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete pecific package data
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var package = await _packageService.DeletePackage(id);
            if (package is null)
            {
                return NotFound("Package not found");
            }
            return Ok("Package deleted successfully");
        }
    }
}
