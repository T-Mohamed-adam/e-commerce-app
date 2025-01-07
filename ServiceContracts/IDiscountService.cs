using TagerProject.Models.Dtos.Discount;

namespace TagerProject.ServiceContracts
{
    public interface IDiscountService
    {
        public Task<List<DiscountResponse>> GetAllDiscounts();

        public Task<DiscountResponse?> GetDiscountById(int id);

        public Task<DiscountResponse?> AddDiscount(DiscountAddRequest discountAddRequest);

        public Task<DiscountResponse?> UpdateDiscount(int id, DiscountUpdateRequest discountUpdateRequest);

        public Task<DiscountResponse?> DeleteDiscount(int id);
    }
}
