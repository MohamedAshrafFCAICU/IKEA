using LinkDev.IKEA.DAL.Entities.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinkDev.IKEA.DAL.Entities.Employee
{
    public class Employee : ModelBase
    {
        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string? Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public string? Image { get; set; }


        public EmplyeeType EmplyeeType { get; set; }

        // Navigational Property [ONE]
        public virtual Department.Department? Department { get; set; }

        public int? DepartmentId { get; set; }

    }
}
