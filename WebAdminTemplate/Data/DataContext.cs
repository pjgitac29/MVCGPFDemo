
using Microsoft.EntityFrameworkCore;
using WebAdminTemplate.Model;

namespace WebAdminTemplate.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<GPFData> GPFData { get; set; }
    }
}
