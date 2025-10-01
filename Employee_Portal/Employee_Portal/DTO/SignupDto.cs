using System.ComponentModel.DataAnnotations;

namespace Employee_Portal.DTO
{
    public class SignupDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        public string PasswordHash { get; set; }
    }
}
