using Microsoft.AspNetCore.Identity;

namespace Hero.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public CorporateCompany CorporateCompany { get; set; }
    }
}
