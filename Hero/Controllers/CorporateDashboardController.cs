using Hero.Models.ViewModels;
using Hero.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hero.Controllers
{
    [Authorize(Roles = "Corporate")]
    public class CorporateDashboardController : Controller
    {
        private readonly ICorporateEmissionService _corporateService;

        public CorporateDashboardController(ICorporateEmissionService corporateService)
        {
            _corporateService = corporateService;
        }

        public async Task<IActionResult> Index()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var company = await _corporateService.GetCompanyForUserAsync(userId);

            //if (company == null)
            //    return NotFound("Company not found for current user.");

            //var emissions = await _corporateService.GetCompanyEmissionsAsync(company.Name);

            //if (emissions == null)
            //    return NotFound("No emissions found for this company.");

            //return View(emissions);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var company = await _corporateService.GetCompanyForUserAsync(userId);

            if (company == null)
            {
                TempData["InfoMessage"] = "Please register your company information first.";
                return RedirectToAction("Submit", "Corporate"); // ← تم التعديل هنا
            }

            var emissions = await _corporateService.GetCompanyEmissionsAsync(company.Name);

            if (emissions == null || !emissions.Any())
                TempData["InfoMessage"] = "No emissions found yet.";

            return View(emissions);
        }
    }
}
