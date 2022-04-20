using DapperDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperDemo.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Company> Companies { get; set; }
    }

}
