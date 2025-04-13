using EmployeeAdminPortal.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Data
{

    // using Dbcontext to interact with the database and also perform crud operations
    public class ApplicationDbContext :DbContext
    {  
        // creating a constructor to initiaze my AppDbContext and using DbContextOptions to tell my App what type of db am using
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        //using the DbSet to represent a table or entity in the database
        public DbSet<Employee> Employees { get; set; }

    }
}
