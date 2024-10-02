using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class SignUpViewModel
	{

        [Display (Name = "First Name")]
        public string FName { get; set; } = null!;

        [Display (Name = "Last Name")]
        public string LName { get; set; } = null!;

        [Required (ErrorMessage = "User Name ")]
        public string UserName { get; set; } = null!;


        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;


        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password) , ErrorMessage = "Confirm Password doesn't Match With Password")]
        public string ConfirmPassword { get; set; } = null!;

		public bool IsAgree { get; set; }

    }
}
