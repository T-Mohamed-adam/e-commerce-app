using TagerProject.Models.Dtos.Pagination;
using TagerProject.Models.Dtos.Purchase;

namespace TagerProject.ServiceContracts
{
    public interface IPurchaseService
    {
        public Task<List<PurchaseResponse>> GetAllPurchases();

        public Task<PurchaseResponse?> GetPurchaseById(int id);

        public Task<PurchaseResponse?> AddPurchase(PurchaseAddRequest purchaseAddRequest);

        public Task<PurchaseResponse?> DeletePurchase(int id);

        public Task<PaginatedResponse<PurchaseResponse>> GetPurchasesList(PaginationRequest request);
    }
}
