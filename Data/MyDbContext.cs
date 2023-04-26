using Microsoft.EntityFrameworkCore;
using MVCFirstProject.Models.Domain;

namespace MVCFirstProject.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
