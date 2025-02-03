using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Inventory;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public InventoryService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<InventoryResponse>> GetAllInventories()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var inventories = await _dbContext.Inventories
                .Include(i => i.Product)
                .Where(i => i.MembershipNumber == membershipNumber)
                .ToListAsync();

            return _mapper.Map<List<InventoryResponse>>(inventories);
        }

        public async Task<InventoryResponse?> GetInventyById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var inventory = await _dbContext.Inventories
                .Include(i => i.Product)
                .Where(i => i.MembershipNumber == membershipNumber && i.Id == id)
                .FirstOrDefaultAsync();

            if (inventory is null) 
            {
                return null;
            }

            return _mapper.Map<InventoryResponse>(inventory);
        }

        public async Task<InventoryResponse?> DeleteInventory(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var inventory = await _dbContext.Inventories
                .Where(i => i.MembershipNumber == membershipNumber && i.Id == id)
                .FirstOrDefaultAsync();

            if (inventory is null)
            {
                return null;
            }

            _dbContext.Inventories.Remove(inventory);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<InventoryResponse>(inventory);
        }

        public async Task IncreaseInventoryQunatity(int productId, int quantity)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var inventory = await _dbContext.Inventories
               .Include(i => i.Product)
               .Where(i => i.MembershipNumber == membershipNumber && i.ProductId == productId)
               .FirstOrDefaultAsync();

            if (inventory is not null) 
            {
                inventory.Quantity += quantity;
                inventory.TotalPurchasePrice = inventory.Quantity * inventory.PurchaseUnitPrice;
                inventory.TotalSalesPrice = inventory.Quantity * inventory.SalesUnitPrice;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DecreaseInventoryQunatity(int productId, int quantity)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var inventory = await _dbContext.Inventories
               .Include(i => i.Product)
               .Where(i => i.MembershipNumber == membershipNumber && i.ProductId == productId)
               .FirstOrDefaultAsync();

            if (inventory is not null)
            {
                inventory.Quantity -= quantity;
                inventory.TotalPurchasePrice = inventory.Quantity * inventory.PurchaseUnitPrice;
                inventory.TotalSalesPrice = inventory.Quantity * inventory.SalesUnitPrice;
            }

             await _dbContext.SaveChangesAsync();
        }
    }
}
