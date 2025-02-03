using TagerProject.Models.Dtos.Inventory;

namespace TagerProject.ServiceContracts
{
    public interface IInventoryService
    {
        public Task<List<InventoryResponse>> GetAllInventories();
        public Task<InventoryResponse?> GetInventyById(int id);
        public Task<InventoryResponse?> DeleteInventory(int id);

        public Task IncreaseInventoryQunatity(int productId, int quantity);
        public Task DecreaseInventoryQunatity(int productId, int quantity);

    }

}
