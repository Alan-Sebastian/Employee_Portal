using System.Collections.Generic;
using AutoMapper;
using Employee_Portal.DTO;
using Employee_Portal.Interfaces;
using Employee_Portal.Models;
using Employee_Portal.Response;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Portal.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IMapper mapper;
        private EmployeeContext dbcontext;

        public EmployeeService(IMapper _mapper, EmployeeContext context)
        {
            mapper = _mapper;
            dbcontext = context;

        }


        public async Task<ResponseData> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var response = new ResponseData();
            if (employeeDto == null)
            {

                response.StatusCode = 400;
                response.Message = "Employee Data not Found";
                response.Data = new { };
                return response;

            }
            else
            {


                var employee = mapper.Map<Employee>(employeeDto);

                await dbcontext.Employees.AddAsync(employee);
                await dbcontext.SaveChangesAsync();


                response.StatusCode = 200;
                response.Message = "Employee Registered Successfully";
                response.Data = employee;

                return response;
            }
        }


        public async Task<ResponseData> GetEmployeeAsync(int employeeId)
        {
            var response = new ResponseData();
            IEnumerable<Employee> employee = await dbcontext.Employees
                                .Where(e => e.EmployeeId == employeeId)
                                .ToListAsync();

            if (employee == null)
            {
                response.StatusCode = 400;
                response.Message = "Employee Data not Found";
                response.Data = new { };
                return response;
            }
            else
            {
                response.StatusCode = 200;
                response.Message = "Employee Details Received Successfully";
                response.Data = employee;
                return response;

            }
        }


        public async Task<ResponseData> GetAllEmployee()
        {
            var response = new ResponseData();

            var employees = await dbcontext.Employees.ToListAsync();

            if (employees != null)
            {
                response.StatusCode = 200;
                response.Message = "Employee Details Received Successfully";
                response.Data = employees;
                return response;

            }
            else
            {
                response.StatusCode = 400;
                response.Message = "Employee Data not Found";
                response.Data = new { };
                return response;
            }
        }


        public async Task<ResponseData> UpdateEmployeeAsync(int id,UpdateEmployeeDto updatedto)
        {
            var response = new ResponseData();



            if (updatedto == null)
            {
                return new ResponseData
                {
                    StatusCode = 400,
                    Message = "No data provided",
                    Data = new { }
                };
            }

            var employee = await dbcontext.Employees.FindAsync(id);

            if (employee == null)
            {
                return new ResponseData
                {
                    StatusCode = 400,
                    Message = "Employee not found",
                    Data = new { }
                };
            }
           
            mapper.Map(updatedto, employee);  

            await dbcontext.SaveChangesAsync();

            return new ResponseData
            {
                StatusCode = 200,
                Message = "Employee updated successfully",
                Data = employee
            };
        }

        public async Task<ResponseData> DeleteEmployeeAsync(int id)
        {
            var employee = await dbcontext.Employees.FindAsync(id);
            if (employee == null)
            {
                return new ResponseData
                {
                    StatusCode = 404,
                    Message = "Employee not found",
                    Data = new { }
                };
            }
            else
            {
                dbcontext.Employees.Remove(employee);
                await dbcontext.SaveChangesAsync();
                return new ResponseData
                {
                    StatusCode = 200,
                    Message = "Employee deleted successfully",
                    Data = new { employeeId = id }
                };
            }
             
            
        }
    }
}

