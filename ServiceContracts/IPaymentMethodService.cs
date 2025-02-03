using TagerProject.Models.Dtos.PaymentMethod;

namespace TagerProject.ServiceContracts
{
    public interface IPaymentMethodService
    {
        public Task<List<PaymentMethodResponse>> GetAllPaymentMethods();

        public Task<PaymentMethodResponse?> GetPaymentMethodById(int id);

        public Task<PaymentMethodResponse?> AddPaymentMethod(PaymentMethodAddRequest paymentMethodAddRequest);

        public Task<PaymentMethodResponse?> UpdatePaymentMethod(int id, PaymentMethodUpdateRequest paymentMethodUpdateRequest);

        public Task<PaymentMethodResponse?> DeletePaymentMethod(int id);
    }
}
