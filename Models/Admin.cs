namespace PhotographyWebsite.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Consider using hashed passwords
    }
}
