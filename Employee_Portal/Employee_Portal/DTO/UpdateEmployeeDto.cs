namespace Employee_Portal.DTO
{
    public class UpdateEmployeeDto
    {
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Gender { get; set; }
        public string? EmployeeType { get; set; }
        public long? Salary { get; set; }
    }
}
