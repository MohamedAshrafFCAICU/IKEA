﻿using LinkDev.IKEA.DAL.Entities.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public class EmployeeDetailsToReturnDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public string? Image { get; set; }


        public EmplyeeType EmplyeeType { get; set; }

        public int? DepartmentId { get; set; }


        #region Adminstration
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }
        #endregion

        public string? Department { get; set; }

    }
}
