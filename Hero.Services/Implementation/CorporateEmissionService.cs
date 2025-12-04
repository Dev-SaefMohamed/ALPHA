//using Hero.Data.Models;
using Hero.Data.Repositories;
using Hero.Models.Entities;
using Hero.Models.ViewModels;
//using Hero.Repositories.Interfaces;
using Hero.Services.Interface;
//using Hero.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hero.Services.Implementation
{
    public class CorporateEmissionService : ICorporateEmissionService
    {
        private readonly ICorporateEmissionRepository _repo;

        public CorporateEmissionService(ICorporateEmissionRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<CorporateEmissionResultViewModel> CalculateEmissionsAsync(CorporateEmissionInputViewModel input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));


            double electricityEmissions = input.ElectricityKWh * 0.55;
            double fuelEmissions = input.FuelLiters * 2.3;
            double transportEmissions = input.TransportKm * 0.21;
            double wasteEmissions = input.WasteKg * 6.0;

            return new CorporateEmissionResultViewModel
            {
                ElectricityEmissions = electricityEmissions,
                FuelEmissions = fuelEmissions,
                TransportEmissions = transportEmissions,
                WasteEmissions = wasteEmissions,
                TotalEmissions = electricityEmissions + fuelEmissions + transportEmissions + wasteEmissions
            };
        }

        public async Task SaveEmissionAsync(CorporateEmissionResultViewModel result, CorporateEmissionInputViewModel input, string userId)
        {
            var company = await _repo.GetCompanyByUserIdAsync(userId);

            if (company == null)
                throw new ArgumentException("Company not found for user.");

            // -------------------------------
            // Prevent duplicate entry in same month
            // -------------------------------
            var existing = await _repo.GetEmissionByCompanyYearMonthAsync(
                company.Id,
                input.Year,
                input.Month
            );

            if (existing != null && userId != "ADMIN")
            {
                throw new InvalidOperationException("You have already submitted emissions for this month.");
            }

            var emission = new CorporateEmission
            {
                CorporateCompanyId = company.Id,
                Year = input.Year,
                Month = input.Month,
                ElectricityKWh = input.ElectricityKWh,
                FuelLiters = input.FuelLiters,
                TransportKm = input.TransportKm,
                WasteKg = input.WasteKg,
                ElectricityEmissions = result.ElectricityEmissions,
                FuelEmissions = result.FuelEmissions,
                TransportEmissions = result.TransportEmissions,
                WasteEmissions = result.WasteEmissions
            };

            await _repo.AddEmissionAsync(emission);
        }



        public async Task<IEnumerable<CorporateEmissionResultViewModel>> GetCompanyEmissionsAsync(string companyName)
        {
            var emissions = await _repo.GetEmissionsByCompanyNameAsync(companyName);

            return emissions.Select(e => new CorporateEmissionResultViewModel
            {
                Year = e.Year,
                Month = e.Month,
                ElectricityEmissions = e.ElectricityEmissions,
                FuelEmissions = e.FuelEmissions,
                TransportEmissions = e.TransportEmissions,
                WasteEmissions = e.WasteEmissions,
                TotalEmissions = e.ElectricityEmissions + e.FuelEmissions + e.TransportEmissions + e.WasteEmissions
            });
        }

        public async Task<CorporateCompany?> GetCompanyForUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;

            return await _repo.GetCompanyByUserIdAsync(userId);
        }

        ////////////////////////////////////////////////////////////////////////////////////
        public async Task<CorporateEmission> AddEmissionAsync(CorporateEmission input)
        {
           
            return await _repo.AddAsync(input);
        }

        public async Task<IEnumerable<CorporateEmission>> GetEmissionsByCompanyIdAsync(int companyId)
        {
            return await _repo.GetByCompanyIdAsync(companyId);
        }

    }
}
