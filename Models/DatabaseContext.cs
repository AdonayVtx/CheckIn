using Microsoft.EntityFrameworkCore;
using CheckIn.Models;

namespace CheckIn.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Documentation> Documentations { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
