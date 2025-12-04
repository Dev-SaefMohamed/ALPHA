
//namespace Hero.Areas.Identity.Pages.Account
//{
//    public class RegisterModel : PageModel
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;



//        [BindProperty]
//        public InputModel Input { get; set; }

//        public class InputModel
//        {
//            [Required]
//            [EmailAddress]
//            [Display(Name = "Email")]
//            public string Email { get; set; }

//            [Required]
//            [Display(Name = "Password")]
//            [DataType(DataType.Password)]
//            public string Password { get; set; }

//            [Required]
//            [Display(Name = "Confirm Password")]
//            [DataType(DataType.Password)]
//            [Compare("Password", ErrorMessage = "Passwords do not match.")]
//            public string ConfirmPassword { get; set; }

//            // ================= Corporate fields =================
//            [Required]
//            [Display(Name = "Company Name")]
//            public string CompanyName { get; set; }

//            [Required]
//            [Display(Name = "Industry")]
//            public string Industry { get; set; }
//        }

//        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
//        {
//            if (!ModelState.IsValid)
//                return Page();

//            var user = new ApplicationUser
//            {
//                UserName = Input.Email,
//                Email = Input.Email
//            };

//            var result = await _userManager.CreateAsync(user, Input.Password);

//            //if (result.Succeeded)
//            //{
//            //    // ================= Create CorporateCompany =================
//            //    var company = new CorporateCompany
//            //    {
//            //        Name = Input.CompanyName,
//            //        Industry = Input.Industry,
//            //        Email = Input.Email,
//            //        UserId = user.Id
//            //    };

//            //    // Add company to DbContext
//            //    // Assuming you inject ApplicationDbContext
//            //    _context.CorporateCompanies.Add(company);
//            //    await _context.SaveChangesAsync();

//            //    await _signInManager.SignInAsync(user, isPersistent: false);
//            //    return LocalRedirect(returnUrl ?? "/");
//            //}

//            if (result.Succeeded)
//            {
//                // 
//                var company = new CorporateCompany
//                {
//                    Name = Input.CompanyName,
//                    Industry = Input.Industry,
//                    Email = Input.Email,
//                    UserId = user.Id
//                };

//                _context.CorporateCompanies.Add(company);
//                await _context.SaveChangesAsync();

//                // 
//                await _userManager.AddToRoleAsync(user, "Corporate");

//                await _signInManager.SignInAsync(user, isPersistent: false);
//                return LocalRedirect(returnUrl ?? "/");
//            }


//            foreach (var error in result.Errors)
//                ModelState.AddModelError(string.Empty, error.Description);

//            return Page();
//        }

//        // Inject DbContext
//        private readonly Hero.Data.ApplicationDbContext _context;
//        public RegisterModel(Hero.Data.ApplicationDbContext context,
//                             UserManager<ApplicationUser> userManager,
//                             SignInManager<ApplicationUser> signInManager)
//        {
//            _context = context;
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }
//    }
//}

using Hero.Data;
using Hero.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Hero.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context,
                             UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string CompanyName { get; set; }

            [Required]
            public string Industry { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return Page();

            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                // Add user to Corporate role
                await _userManager.AddToRoleAsync(user, "Corporate");

                // Create corporate company linked to user
                var company = new CorporateCompany
                {
                    Name = Input.CompanyName,
                    Industry = Input.Industry,
                    Email = Input.Email,
                    UserId = user.Id
                };

                _context.CorporateCompanies.Add(company);
                await _context.SaveChangesAsync();

                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl ?? "/");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}
