using AutoMapper;
using Employee_Portal.DTO;
using Employee_Portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Employee_Portal.Response;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Employee_Portal.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Employee_Portal.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public static User user = new();

        private readonly IMapper _mapper;

        private readonly EmployeeContext db;

        private IConfiguration config;

        private IUserService userService;


        public AuthController(IMapper mapper, EmployeeContext _db,IConfiguration _config,IUserService userservice)
        {
            _mapper = mapper;
            db = _db;
            config = _config;
            userService = userservice;
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseData>> RegisterUser(SignupDto request)
        {

            var response=await  userService.RegisterAsync(request);

            return response;
   
        }

        [AllowAnonymous]
        [HttpPost("Login")]

        public async Task<ActionResult<ResponseData>> Login(LoginDto request)
        {
            var response = await userService.LoginAsync(request);

            if (response == null)
                return NotFound();

            return response;

        }


       







    }
}
