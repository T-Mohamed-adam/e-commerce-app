using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TagerProject.Exceptions;
using TagerProject.Models.Dtos.Order;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    // localhost:xxxx/api/orders
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        // Get all orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();

            return Ok(orders);
        }

        // Get all orders
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id) 
        {
            var order = await _orderService.GetOrderById(id);

            if (order is null) 
            {
                return NotFound("Order not found");
            }

            return Ok(order);
        }

        // Initiate new order
        [HttpPost]
        public async Task<IActionResult> InitiateOrder(OrderAddRequest orderAddRequest)
        {
            try
            {
                var order = await _orderService.InitiateOrder(orderAddRequest);
                return Ok(order);
            }
            catch (InsufficientQuantityException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        [HttpPost("paid/{id:int}")]
        public async Task<IActionResult> PaidOrder(int id, OrderPayment orderPayment) 
        {
            var order = await _orderService.PaidOrder(id, orderPayment);

            if (order is null) 
            {
                return NotFound("Order not found");
            }

            return Ok(order);   
        }

        // Cancel order 
        [HttpPost("cancel/{id:int}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _orderService.CancelOrder(id);

            if (order is null)
            {
                return NotFound("Order not found");
            }

            return Ok(order);
        }

        // Complete order
        [HttpPost("complete/{id:int}")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            var order = await _orderService.CompleteOrder(id);

            if (order is null)
            {
                return NotFound("Order not found");
            }

            return Ok(order);
        }

        // delete order
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.DeleteOrder(id);

            if (order is null)
            {
                return NotFound("Order not found");
            }

            return Ok("Order deleted successfully");
        }


    }
}
