using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Employee_Portal.DTO;
using Employee_Portal.Interfaces;
using Employee_Portal.Models;
using Employee_Portal.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Employee_Portal.Services
{
    public class UserService : IUserService
    {

        private readonly IMapper mapper;

        private EmployeeContext dbcontext;

        private readonly User user = new();

        private IConfiguration config;


        public UserService(IMapper _mapper,EmployeeContext context, IConfiguration _config)
        { 
            mapper = _mapper;
            dbcontext = context;
            config = _config;
        
        }
        public async Task<ResponseData> RegisterAsync(SignupDto request)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.PasswordHash);

            var employee = mapper.Map<User>(request);

            employee.UserId =Guid.NewGuid();
            employee.PasswordHash = hashedPassword;
            employee.UserName = request.UserName;

            ResponseData responseData = new ResponseData();

            if (employee != null && !(await dbcontext.Users.AnyAsync(u => u.UserName == employee.UserName)))
            {
                responseData = new ResponseData()
                {

                    StatusCode = 200,
                    Message = "Employee Registered Successfully",
                    Data = employee 
                };
                await dbcontext.Users.AddAsync(employee);
                await dbcontext.SaveChangesAsync();
                return responseData;
            }
            else if(await dbcontext.Users.AnyAsync(u => u.UserName == employee.UserName))
            {
                responseData = new ResponseData()
                {
                    StatusCode = 409, // Conflict
                    Message = "Username already exists",
                    Data = new { }
                };
                return responseData;
            }
            else
            {
                responseData = new ResponseData()
                {

                    StatusCode = 400,
                    Message = "Bad Request",
                    Data = new { }
                };
                return responseData;
            }
        }


        public async Task<ResponseData> LoginAsync(LoginDto request)
        {
            var userDetails = await dbcontext.Users.FirstOrDefaultAsync(e => e.UserName == request.UserName);

            var loginStatus = new PasswordHasher<User>().VerifyHashedPassword(userDetails, userDetails.PasswordHash, request.PasswordHash);



            if (loginStatus == PasswordVerificationResult.Success)
            {
                var token = generateJsonWebToken(userDetails);

                var responseData = new ResponseData()
                {

                    StatusCode = 200,
                    Message = "Employee Registered Successfully",
                    JwtToken = token,
                    Data = loginStatus
                };
                return responseData;
            }
            else
            {
                var responseData = new ResponseData()
                {

                    StatusCode = 400,
                    Message = "Bad Request",
                    Data = new { }
                };
                return responseData;
            }
        }

        private string generateJsonWebToken(User userdetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.PreferredUsername,userdetails.UserName)
        };
            var tokenDescriptor = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
