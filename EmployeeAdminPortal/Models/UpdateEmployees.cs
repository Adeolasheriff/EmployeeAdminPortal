﻿namespace EmployeeAdminPortal.Models
{
    public class UpdateEmployees
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }


        public decimal Salary { get; set; }
    }
}
