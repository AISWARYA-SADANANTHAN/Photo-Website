using System.ComponentModel.DataAnnotations;

namespace PhotographyWebsite.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Consider using hashed passwords
    
}
}
