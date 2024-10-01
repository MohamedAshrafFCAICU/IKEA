using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

	

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet] // GET: /Account/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost] // POST
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user  = await _userManager.FindByNameAsync(model.UserName);

            if (user is { } )
            {


                  ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This Username Exists");
                  return View(model);

            
               
            }
	       user = new ApplicationUser
	        {
		        FName = model.FName,
		        LName = model.LName,
		        UserName = model.UserName,
		        Email = model.Email,
		        IsAgree = model.IsAgree,
	        };

	        var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(SignIn));

            foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
              
            return View(model); 
        }
    }
}
