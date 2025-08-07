using Microsoft.EntityFrameworkCore;
using PhotographyWebsite.Models;

namespace PhotographyWebsite.Data
{ 

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Admin> Admins { get; set; }    
    public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=PhotoDataBase;Integrated Security=True;TrustServerCertificate=True;");
        }
    }
}