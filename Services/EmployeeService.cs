using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Helpers;
using TagerProject.Models.Dtos.Employee;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MembershipNumberHelper _membershipNumberHelper;
        private readonly PasswordHashingHelper _passwordHasher;
        private readonly IMapper _mapper;

        public EmployeeService(ApplicationDbContext dbContext, MembershipNumberHelper membershipNumberHelper,
            PasswordHashingHelper passwordHashingHelper, IMapper mapper) 
        {
            _dbContext = dbContext;
            _membershipNumberHelper = membershipNumberHelper;
            _passwordHasher = passwordHashingHelper;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponse>> GetAllEmoplyees()
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var employees = await _dbContext.Employees
                .Where(e => e.MembershipNumber == membershipNumber && e.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<EmployeeResponse>>(employees);
        }

        public async Task<EmployeeResponse?> GetEmployeeById(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var employee = await _dbContext.Employees
                .Where(e => e.MembershipNumber == membershipNumber && e.IsDeleted == false)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee is null)
            {
                return null;
            }

            return _mapper.Map<EmployeeResponse>(employee);
        }

        public async Task<EmployeeResponse?> AddEmployee(EmployeeAddRequest employeeAddRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            Employee employee = new Employee() 
            {
                FirstName = employeeAddRequest.FirstName,
                LastName = employeeAddRequest.LastName,
                PhoneNumber = employeeAddRequest.PhoneNumber,
                Email = employeeAddRequest.Email,
                Address = employeeAddRequest.Address,
                CityId = employeeAddRequest.CityId,
                Password = _passwordHasher.HashPassword(employeeAddRequest.Password!),
                MembershipNumber = membershipNumber,

            };

            await _dbContext.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<EmployeeResponse>(employee);
        }

 
        public async Task<EmployeeResponse?> UpdateEmployee(int id, EmployeeUpdateRequest employeeUpdateRequest)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var employee = await _dbContext.Employees
                .Where(e => e.MembershipNumber == membershipNumber && e.IsDeleted == false)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee is null)
            {
                return null;
            }

            employee.FirstName = employeeUpdateRequest.FirstName;
            employee.LastName = employeeUpdateRequest.LastName;
            employee.PhoneNumber = employeeUpdateRequest.PhoneNumber;
            employee.Email = employeeUpdateRequest.Email;
            employee.Address = employeeUpdateRequest.Address;
            employee.CityId = employeeUpdateRequest.CityId;
            employee.IsActive = employeeUpdateRequest.IsActive;
            employee.Password = _passwordHasher.HashPassword(employeeUpdateRequest.Password!);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<EmployeeResponse>(employee);
        }

        public async Task<EmployeeResponse?> DeleteEmployee(int id)
        {
            var membershipNumber = _membershipNumberHelper.GetMembershipNumber();

            var employee = await _dbContext.Employees
                .Where(e => e.MembershipNumber == membershipNumber && e.IsDeleted == false)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee is null)
            {
                return null;
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<EmployeeResponse>(employee);
        }
    }
}
