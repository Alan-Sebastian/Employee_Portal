using Employee_Portal.Response;

using Employee_Portal.DTO;

namespace Employee_Portal.Interfaces
{
    public interface IUserService
    {

        Task<ResponseData> RegisterAsync(SignupDto dto);

        Task<ResponseData> LoginAsync(LoginDto loginDto);
    }
}
