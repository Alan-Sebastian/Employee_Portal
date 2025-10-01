using Employee_Portal.DTO;
using Employee_Portal.Response;
using Microsoft.AspNetCore.JsonPatch;

namespace Employee_Portal.Interfaces
{
    public interface IEmployeeService
    {

        Task<ResponseData> AddEmployeeAsync(EmployeeDto dto);

        Task<ResponseData> GetEmployeeAsync(int employeeId);

        Task<ResponseData> GetAllEmployee();

        Task<ResponseData> UpdateEmployeeAsync(int id,UpdateEmployeeDto updatedto);

        Task<ResponseData> DeleteEmployeeAsync(int id);

    }
}
