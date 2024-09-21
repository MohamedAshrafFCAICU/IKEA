﻿using LinkDev.IKEA.DAL.Entities.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Employees
{
    public class EmplyeeEditViewModel
    {

        //[Required]
        [MaxLength(50, ErrorMessage = "Max Length Of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length Of Name is 5 Chars")]
        public string Name { get; set; } = null!;

        [Range(22, 30)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
                                , ErrorMessage = "Adderss Must be Like 123-Street-City-Country")]
        public string? Address { get; set; }

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmplyeeType EmplyeeType { get; set; }
    }
}
