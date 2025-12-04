using Hero.Models.Entities;
using Hero.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hero.Services.Interface
{
    public interface ICorporateEmissionService
    {
        //Task SubmitCorporateEmissionAsync(CorporateEmission model, string userId);

        Task<CorporateEmissionResultViewModel> CalculateEmissionsAsync(CorporateEmissionInputViewModel input);

        Task SaveEmissionAsync(CorporateEmissionResultViewModel result, CorporateEmissionInputViewModel input, string userId);

        Task<IEnumerable<CorporateEmissionResultViewModel>> GetCompanyEmissionsAsync(string companyName);

        Task<CorporateCompany?> GetCompanyForUserAsync(string userId);

        // new
        Task<CorporateEmission> AddEmissionAsync(CorporateEmission input);
        // new
        Task<IEnumerable<CorporateEmission>> GetEmissionsByCompanyIdAsync(int companyId);
    }
}
