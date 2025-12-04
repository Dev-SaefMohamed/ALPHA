
using Hero.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hero.Models.Entities
{
    public class CorporateEmissionCoefficient
    {

        public int Id { get; set; }

        public CategoryType Category { get; set; }

        public TransportationType? TransportationType { get; set; }

        public double Coefficient { get; set; }

        [StringLength(100)]
        public string? Unit { get; set; }

        [StringLength(200)]
        public string? Source { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public DataQuality DataQuality { get; set; }  // NEW

    }
}
