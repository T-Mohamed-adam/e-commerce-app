using TagerProject.Models.Dtos.Supplier;

namespace TagerProject.ServiceContracts
{
    public interface ISupplierService
    {
        public Task<List<SupplierResponse>> GetAllSuppliers();

        public Task<SupplierResponse?> GetSupplierById(int id);

        public Task<SupplierResponse?> AddSupplier(SupplierAddRequest supplierAddRequest);

        public Task<SupplierResponse?> UpdateSupplier(int id, SupplierUpdateRequest supplierUpdateRequest);

        public Task<SupplierResponse?> DeleteSupplier(int id);
    }
}
