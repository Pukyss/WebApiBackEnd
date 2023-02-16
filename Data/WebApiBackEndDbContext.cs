using Microsoft.EntityFrameworkCore;
using WebApiBackEnd.Models;

namespace WebApiBackEnd.Data
{
    public class WebApiBackEndDbContext : DbContext
    {
        public WebApiBackEndDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
