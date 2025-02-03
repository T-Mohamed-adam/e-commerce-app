using TagerProject.Models.Dtos.PaymentType;

namespace TagerProject.ServiceContracts
{
    public interface IPaymentTypeService
    {
        public Task<List<PaymentTypeResponse>> GetAllPaymentTypes();

        public Task<PaymentTypeResponse?> GetPaymentTypeById(int id);

        public Task<PaymentTypeResponse?> AddPaymentType(PaymentTypeAddRequest paymentTypeAddRequest);

        public Task<PaymentTypeResponse?> UpdatePaymentType(int id, PaymentTypeUpdateRequest paymentTypeUpdateRequest);

        public Task<PaymentTypeResponse?> DeletePaymentType(int id);

    }
}
