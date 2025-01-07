using TagerProject.Models.Dtos.Tax;

namespace TagerProject.ServiceContracts
{
    public interface ITaxService
    {
        public Task<List<TaxResponse>> GetAllTaxes();
        public Task<TaxResponse?> GetTaxById(int id);
        public Task<TaxResponse?> AddTax(TaxAddRequest taxAddRequest);
        public Task<TaxResponse?>UpdateTax(int id, TaxUpdateRequest taxUpdateRequest);
        public Task<TaxResponse?> DeleteTax(int id);

    }
}
