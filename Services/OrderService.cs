using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TagerProject.Data;
using TagerProject.Exceptions;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Order;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;
        private readonly IInventoryService _inventoryService;

        public OrderService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper,
            IMapper mapper, IInventoryService inventoryService) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
            _inventoryService = inventoryService;
        }
        public async Task<List<OrderResponse>> GetAllOrders() 
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            var orders = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.MembershipNumber == membershipNumber)
                .ToListAsync();

            return _mapper.Map<List<OrderResponse>>(orders);
        }

        public async Task<OrderResponse?> GetOrderById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.MembershipNumber == membershipNumber && o.Id == id)
                .FirstOrDefaultAsync();

            if (order == null) 
            {
                return null;
            }

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<OrderResponse?> InitiateOrder(OrderAddRequest orderAddRequest)
        {
            // Add order data
           Order order = AddOrder(orderAddRequest);

            // Extract product IDs and fetch their prices
            var productIds = orderAddRequest.OrderItems!.Select(o => o.ProductId).ToList();
            var products = await _dbContext.Products
                                          .Where(p => productIds.Contains(p.Id))
                                          .ToDictionaryAsync(p => p.Id, p => p.Price); // Assuming Products have a Price field

            decimal totalAmount = 0;
            decimal taxAmount = 0;
            decimal discountAmount =  0; // Use discount from request if provided
            decimal taxRate = 0.15m; // Example tax rate of 10% (replace as needed)

            // Create the order items with the generated OrderId
            var orderItems = orderAddRequest.OrderItems!.Select(o =>
            {
                var unitPrice = products.ContainsKey(o.ProductId) ? products[o.ProductId] : 0;
                var itemTotal = unitPrice * o.Quantity;

                totalAmount += itemTotal;
                taxAmount += itemTotal * taxRate; // Tax calculation based on item total

                /*_inventoryService.DecreaseInventoryQunatity(o.ProductId, o.Quantity);*/
                bool? inventoryDecrease = DecreaseInventoryQunatity(o.ProductId, o.Quantity);

                if (inventoryDecrease == false) 
                {
                    throw new InsufficientQuantityException(o.ProductId);
                }

                // Return the order item with the OrderId set
                return new OrderItem()
                {
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                    UnitPrice = unitPrice,
                };
            }).ToList();

            // Calculate final amount: Total + Tax - Discount
            decimal finalAmount = totalAmount + taxAmount - discountAmount;

            // Set the calculated values to the order
            order.TotalAmount = totalAmount;
            order.TaxAmount = taxAmount;
            order.FinalAmount = finalAmount;

            // Save the order to generate the ID
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync(); // Ensure the Order is saved and gets an Id

            // Now set the OrderId for each order item
            foreach (var orderItem in orderItems)
            {
                orderItem.OrderId = order.Id;
            }

            // Add order items to the database
            await _dbContext.OrderItems.AddRangeAsync(orderItems);
            await _dbContext.SaveChangesAsync(); // Save order items

            return _mapper.Map<OrderResponse>(order);
        }

        public Order AddOrder(OrderAddRequest orderAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            // Create the order object
            Order order = new Order()
            {
                Guid = Guid.NewGuid(),
                DateTime = DateTime.Now,
                CustomerId = orderAddRequest.CustomerId,
                DiscountId = orderAddRequest.DiscountId,
                CouponCode = orderAddRequest.CouponCode,
                MembershipNumber = membershipNumber
            };

            return order;
        }

        public async Task<OrderResponse?> PaidOrder(int id, OrderPayment orderPayment)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var order = await _dbContext.Orders
                .Where(o => o.MembershipNumber == membershipNumber && o.Status == "Pending" && o.Id == id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            order.PaymentMethodId = orderPayment.PaymentMethodId;
            order.PaidAmount = order.FinalAmount;
            order.Paid = true;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(order);


        }

        public async Task<OrderResponse?> CancelOrder(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var order = await _dbContext.Orders
                .Where(o => o.MembershipNumber == membershipNumber && o.Status == "Pending" && o.Id == id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            order.Status = "Canceled";

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<OrderResponse?> CompleteOrder(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.MembershipNumber == membershipNumber && o.Status == "Pending" && o.Paid == true && o.Id == id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            order.Status = "Completed";

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<OrderResponse?> DeleteOrder(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var order = await _dbContext.Orders
                .Where(o => o.MembershipNumber == membershipNumber && o.Id == id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(order);
        }



        private  bool? DecreaseInventoryQunatity(int productId, int quantity)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var inventory =  _dbContext.Inventories
               .Include(i => i.Product)
               .Where(i => i.MembershipNumber == membershipNumber && i.ProductId == productId)
               .FirstOrDefault();

            if (inventory is null || inventory.Quantity < quantity)
            {
                return false;
            }

            inventory.Quantity -= quantity;
            inventory.TotalPurchasePrice = inventory.Quantity * inventory.PurchaseUnitPrice;
            inventory.TotalSalesPrice = inventory.Quantity * inventory.SalesUnitPrice;

            _dbContext.SaveChanges();

            return true;
        }


        public bool? IncreaseInventoryQunatity(int productId, int quantity)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var inventory =  _dbContext.Inventories
               .Include(i => i.Product)
               .Where(i => i.MembershipNumber == membershipNumber && i.ProductId == productId)
               .FirstOrDefault();

            if (inventory is null) 
            {
                return false;
            }
            {
                inventory.Quantity += quantity;
                inventory.TotalPurchasePrice = inventory.Quantity * inventory.PurchaseUnitPrice;
                inventory.TotalSalesPrice = inventory.Quantity * inventory.SalesUnitPrice;
            }

             _dbContext.SaveChanges();
            return true;
        }

    }
}
