using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Customer;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _mapper = mapper;
        }

        public async Task<List<CustomerResponse>> GetAllCustomers()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var customers = await _dbContext.Customers
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse?> GetCustomerById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var customer = await _dbContext.Customers
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null) 
            {
                return null;
            }

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse?> AddCustomer(CustomerAddRequest customerAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Customer customer = new Customer() 
            {
                FirstName = customerAddRequest.FirstName,
                LastName = customerAddRequest.LastName,
                PhoneNumber = customerAddRequest.PhoneNumber,
                Email = customerAddRequest.Email,
                Address = customerAddRequest.Address,
                CityId = customerAddRequest.CityId,
                MembershipNumber = membershipNumber,
            };

            await _dbContext.Customers.AddRangeAsync(customer);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse?> UpdateCustomer(int id, CustomerUpdateRequest customerUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var customer = await _dbContext.Customers
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                return null;
            }

            customer.FirstName = customerUpdateRequest.FirstName;
            customer.LastName = customerUpdateRequest.LastName;
            customer.PhoneNumber = customerUpdateRequest.PhoneNumber;
            customer.Email = customerUpdateRequest.Email;
            customer.Address = customerUpdateRequest.Address;
            customer.CityId = customerUpdateRequest.CityId;
            customer.IsActive = customerUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse?> DeleteCustomer(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var customer = await _dbContext.Customers
                .Where(c => c.MembershipNumber == membershipNumber && c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                return null;
            }

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }
    }
}
