using System.ComponentModel.DataAnnotations;

namespace Employee_Portal.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Department { get;set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public string  EmployeeType { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public long Salary { get; set; }
    }
}
