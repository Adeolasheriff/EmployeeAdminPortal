using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // database context for data Access

        private readonly ApplicationDbContext _ApplicationDbContext;
        public EmployeeController(ApplicationDbContext applicationDbContext)
        {
            // _ApplicationDbContext is configured and will be used to perform our crud operations

            _ApplicationDbContext = applicationDbContext;
        }

        [HttpGet]

        // get all users in d Database
        public async Task<IActionResult> Get()
        {
            // fetch all employee in the db

            var Employees = await _ApplicationDbContext.Employees.ToListAsync();

            //  if user does not exist in the database return not found

            if (Employees == null)
            {

                return NotFound("not user found in the db");

            }

            // return ok status

            return Ok(Employees);

        }

        [HttpGet]
        [Route("{Id:guid}")]

        // getting user indivdually by id
        public async Task<IActionResult> GetEmployeeById(Guid Id)
        {
            var employe = await _ApplicationDbContext.Employees.FindAsync(Id);
            //var employe = await _ApplicationDbContext.Employees.FirstOrDefaultAsync(e => e.Id == Id);
            if (employe == null)
            {
                return NotFound("user not found");
            }
            return Ok(employe);
        }





        // route is post since im creating a new Db

        [HttpPost]

        public async Task<IActionResult> AddEmployee(AddEmployee addEmployee)
        {
            // checking to see if the user already exist in the database

            var existingUser = await _ApplicationDbContext.Employees.FirstOrDefaultAsync(User => User.Email == addEmployee.Email);
            if (existingUser != null)
            {
                return Conflict("user already exist");
            }
            // created a DTO Data Transfer Object

            var employe = new Employee()
            {
                // field required to be input 

                Name = addEmployee.Name,
                Email = addEmployee.Email,
                Phone = addEmployee.Phone,
                Salary = addEmployee.Salary,

            };
            // Adding user to the  db

            _ApplicationDbContext.Employees.Add(employe);
            // saving user details to the db

            await _ApplicationDbContext.SaveChangesAsync();

            // returning ok status 

            return Ok(employe);

        }

        // updating user in the database
        [HttpPut]
        [Route("{Id:guid}")]

        public async Task<IActionResult> UpdateEmploye(Guid Id, UpdateEmployees updateEmployees)
        {
            // find user 
            var employee = await _ApplicationDbContext.Employees.FindAsync(Id);

            // if its equal to null return user not found

            if (employee == null)
            {
                return Conflict("user not found in the database");
            }
            // 
            employee.Name = updateEmployees.Name;
            employee.Email = updateEmployees.Email;
            employee.Phone = updateEmployees.Phone;
            employee.Salary = updateEmployees.Salary;

            // update employee

            _ApplicationDbContext.Employees.Update(employee);

            // save changed detail to the db

            await _ApplicationDbContext.SaveChangesAsync();

            // return ok status

            return Ok(employee);
        }

        // Delete user

        [HttpDelete]
        [Route("{Id:guid}")]

        // 
        public async Task<IActionResult> DeleteEmployee(Guid Id)
        {
            // find user in the db using Id

            var DeleteEmp = await _ApplicationDbContext.Employees.FirstOrDefaultAsync(e => e.Id == Id);

            // if its null return not found
            if (DeleteEmp == null)
            {
                return Conflict("User not found in the db");
            }
            // if found delete user from the db

            _ApplicationDbContext.Employees.Remove(DeleteEmp);

            // save changes to the db

            await _ApplicationDbContext.SaveChangesAsync();

            // return ok status

            return Ok(DeleteEmp);
        }


    }
}