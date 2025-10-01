using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Portal.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int slno { get; set; }

        [Required]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }

        public string PasswordHash { get; set; }
    }
}
