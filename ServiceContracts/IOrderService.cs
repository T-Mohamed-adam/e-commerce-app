using TagerProject.Models.Dtos.Order;

namespace TagerProject.ServiceContracts
{
    public interface IOrderService
    {
        public Task<List<OrderResponse>> GetAllOrders();

        public Task<OrderResponse?> GetOrderById(int id);

        public Task<OrderResponse?> InitiateOrder(OrderAddRequest orderAddRequest);

        public Task<OrderResponse?> PaidOrder(int id, OrderPayment orderPayment);

        public Task<OrderResponse?> CompleteOrder(int id);

        public Task<OrderResponse?> CancelOrder(int id);

        public Task<OrderResponse?> DeleteOrder(int id);
    }
}
