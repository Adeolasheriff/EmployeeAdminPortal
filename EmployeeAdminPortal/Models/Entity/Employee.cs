namespace EmployeeAdminPortal.Models.Entity
{
    public class Employee
    {
        // guid as the unique identifier
        public Guid Id { get; set; }

        // required indicate the property is required or compulsory
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }


        public decimal Salary { get; set; }
    }
}
