using Hero.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hero.Data.Repositories
{
    public interface ICorporateEmissionRepository
    {
        // get company details by user id
        Task<CorporateCompany?> GetCompanyByUserIdAsync(string userId);

        // 
        Task<IEnumerable<CorporateEmission>> GetEmissionsByCompanyNameAsync(string companyName);

        // 
        Task SaveEmissionAsync(CorporateEmission emission);


        Task AddEmissionAsync(CorporateEmission emission);

        // New method for checking existing emission in the same month
        Task<CorporateEmission?> GetEmissionByCompanyYearMonthAsync(int companyId, int year, int month);

        //////////////////////////////////////////////////////////////////////////////////////
        Task<CorporateEmission> AddAsync(CorporateEmission emission);
        Task<IEnumerable<CorporateEmission>> GetByCompanyIdAsync(int companyId);
    }
}
