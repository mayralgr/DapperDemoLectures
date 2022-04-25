using DapperDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperDemo.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<Company>().Ignore(t => t.Employees);

            modelBuilder.Entity<Employee>()
                .HasOne(c => c.Company).WithMany(e => e.Employees).HasForeignKey(c => c.CompanyId);
        }


        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }

}
