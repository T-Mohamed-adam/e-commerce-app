using TagerProject.Models.Dtos.Employee;

namespace TagerProject.ServiceContracts
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeResponse>> GetAllEmoplyees();

        public Task<EmployeeResponse?> GetEmployeeById(int id);

        public Task<EmployeeResponse?> AddEmployee(EmployeeAddRequest employeeAddRequest);

        public Task<EmployeeResponse?> UpdateEmployee(int id, EmployeeUpdateRequest employeeUpdateRequest);

        public Task<EmployeeResponse?> DeleteEmployee(int id);
    }
}
