using TagerProject.Models.Dtos.Customer;

namespace TagerProject.ServiceContracts
{
    public interface ICustomerService
    {
        public Task<List<CustomerResponse>> GetAllCustomers();

        public Task<CustomerResponse?> GetCustomerById(int id);

        public Task<CustomerResponse?> AddCustomer(CustomerAddRequest customerAddRequest);

        public Task<CustomerResponse?> UpdateCustomer(int id, CustomerUpdateRequest customerUpdateRequest);

        public Task<CustomerResponse?> DeleteCustomer(int id);
    }
}
