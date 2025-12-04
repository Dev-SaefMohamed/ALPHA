using Hero.Data.Repositories;
using Hero.Models.ViewModels;
using Hero.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hero.Web.Controllers
{
    [Authorize(Roles = "Corporate")]
    public class CorporateController : Controller
    {
        private readonly ICorporateEmissionService _corporateService;
        private readonly ICorporateEmissionRepository _corporateRepo;

        public CorporateController(
            ICorporateEmissionService corporateService,
            ICorporateEmissionRepository corporateRepo)
        {
            _corporateService = corporateService;
            _corporateRepo = corporateRepo;
        }

        // GET: /Corporate/Submit
        public async Task<IActionResult> Submit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var company = await _corporateService.GetCompanyForUserAsync(userId);

            var vm = new CorporateEmissionInputViewModel
            {
                CompanyName = company?.Name ?? string.Empty,
                Year = System.DateTime.Now.Year,
                Month = System.DateTime.Now.Month
            };

            return View(vm);
        }

        // POST: /Corporate/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(CorporateEmissionInputViewModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var linkedCompany = await _corporateService.GetCompanyForUserAsync(userId);

            if (linkedCompany != null)
            {
                input.CompanyName = linkedCompany.Name;
            }

            // حساب الانبعاثات
            CorporateEmissionResultViewModel result;
            try
            {
                result = await _corporateService.CalculateEmissionsAsync(input);
            }
            catch (System.ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(input);
            }

            
            await _corporateService.SaveEmissionAsync(result, input, userId);

            TempData["SuccessMessage"] = "Emissions submitted successfully.";
            return RedirectToAction(nameof(Dashboard));
        }

        // GET: /Corporate/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var company = await _corporateService.GetCompanyForUserAsync(userId);

            if (company == null)
            {
                TempData["InfoMessage"] = "Please register your company information first.";
                return RedirectToAction(nameof(Submit));
            }

            var emissions = await _corporateService.GetCompanyEmissionsAsync(company.Name);

            ViewBag.CompanyName = company.Name;
            return View(emissions);
        }

        public IActionResult API()
        {
            return View();
        }

    }
}
