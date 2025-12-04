using Hero.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hero.Data.Repositories
{
    public class CorporateEmissionRepository : ICorporateEmissionRepository
    {
        private readonly ApplicationDbContext _context;

        public CorporateEmissionRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public async Task<CorporateCompany?> GetCompanyByUserIdAsync(string userId)
        {
            return await _context.CorporateCompanies
                                 .Include(c => c.Emissions)
                                 .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<CorporateEmission>> GetEmissionsByCompanyNameAsync(string companyName)
        {
            return await _context.CorporateEmissions
                                 .Include(e => e.Company)
                                 .Where(e => e.Company.Name == companyName)
                                 .ToListAsync();
        }

        public async Task SaveEmissionAsync(CorporateEmission emission)
        {
            _context.CorporateEmissions.Add(emission);
            await _context.SaveChangesAsync();
        }

        

        public async Task AddEmissionAsync(CorporateEmission emission)
        {
            await _context.CorporateEmissions.AddAsync(emission);
            await _context.SaveChangesAsync();
        }

        public async Task<CorporateEmission?> GetEmissionByCompanyYearMonthAsync(int companyId, int year, int month)
        {
            return await _context.CorporateEmissions
                .FirstOrDefaultAsync(e => e.CorporateCompanyId == companyId
                    && e.Year == year
                    && e.Month == month);
        }

        //////////////////////////////////////////////////////////////////////////////////
        public async Task<CorporateEmission> AddAsync(CorporateEmission emission)
        {
            _context.CorporateEmissions.Add(emission);
            await _context.SaveChangesAsync();
            return emission;
        }

        public async Task<IEnumerable<CorporateEmission>> GetByCompanyIdAsync(int companyId)
        {
            return await _context.CorporateEmissions
                .Include(e => e.Company)
                .Where(e => e.CorporateCompanyId == companyId)
                .ToListAsync();
        }
    }
}
