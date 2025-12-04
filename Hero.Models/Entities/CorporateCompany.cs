

namespace Hero.Models.Entities
{
    public class CorporateCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } // Optional
        public string Industry { get; set; } // Optional

        // FK to Identity User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Relation with Emissions
        public ICollection<CorporateEmission> Emissions { get; set; }
    }
}
