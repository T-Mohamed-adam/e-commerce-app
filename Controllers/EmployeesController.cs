using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Employee;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/employees
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }

        // Get all employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
           var employees = await _employeeService.GetAllEmoplyees();

            return Ok(employees);
        }

        // Get employee by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id) 
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee is null) 
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
        }

        // Add new employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeAddRequest employeeAddRequest) 
        {
            try
            {
                var employee = await _employeeService.AddEmployee(employeeAddRequest);
                return Ok(employee);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
         
        }

        // Update specific employee data
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateRequest employeeUpdateRequest) 
        {
            try
            {
                var employee = await _employeeService.UpdateEmployee(id, employeeUpdateRequest);
                if (employee is null)
                {
                    return NotFound("Employee not found");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Update specific employee data
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id) 
        {
            var employee = await _employeeService.DeleteEmployee(id);
            if (employee is null)
            {
                return NotFound("Employee not found");
            }
            return Ok("Employee data deleted successfully");
        }
    }
}
