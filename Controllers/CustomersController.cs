using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Models.Dtos.Customer;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/customers
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService) 
        {
            _customerService = customerService;
        }

        // Get all customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();

            return Ok(customers);
        }

        // Get customer by its id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer is null)
            {
                return NotFound("Customer not found");
            }
            return Ok(customer);
        }

        // Add new customer
        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerAddRequest customerAddRequest)
        {
            try
            {
                var customer = await _customerService.AddCustomer(customerAddRequest);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Update specific customer data
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateRequest customerUpdateRequest)
        {
            try
            {
                var customer = await _customerService.UpdateCustomer(id, customerUpdateRequest);
                if (customer is null)
                {
                    return NotFound("Customer not found");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Update specific customer data
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.DeleteCustomer(id);
            if (customer is null)
            {
                return NotFound("Customer not found");
            }
            return Ok("Customer data deleted successfully");
        }
    }
}
