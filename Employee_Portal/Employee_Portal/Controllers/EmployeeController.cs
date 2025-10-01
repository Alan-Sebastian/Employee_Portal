using Employee_Portal.DTO;
using Employee_Portal.Interfaces;
using Employee_Portal.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Portal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

        }


        [HttpPost("AddEmployee")]
        public async Task<ActionResult<ResponseData>> AddEmployee(EmployeeDto request)
        {
            var response = await _employeeService.AddEmployeeAsync(request);

            if (response == null)
                return NotFound();

            return response;

        }


        [HttpGet("GetEmployee/{employeeId}")]
        public async Task<ActionResult<ResponseData>> GetEmployee(int employeeId)
        {
            var response = await _employeeService.GetEmployeeAsync(employeeId);

            if (response == null)
                return NotFound();

            return response;

        }

        [HttpGet("GetAllEmployee")]
        public async Task<ActionResult<ResponseData>> GetAllEmployee()
        {
            var response = await _employeeService.GetAllEmployee();
            return response;
        }

        [HttpPut("EditEmployee/{id}")]
        public async Task<ActionResult<ResponseData>> EditEmployee([FromRoute] int id, [FromBody] UpdateEmployeeDto updatedto)
        {
         
            var response = await _employeeService.UpdateEmployeeAsync(id, updatedto);
            return response;
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<ActionResult<ResponseData>> DeleteEmployee([FromRoute] int id)
        {

            var response = await _employeeService.DeleteEmployeeAsync(id);
            return response;
        }

    }
}
