using Microsoft.EntityFrameworkCore;

namespace Employee_Portal.Models
{
    public class EmployeeContext :DbContext
    {

      public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
       {

        } 
     

        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }


    }
}
