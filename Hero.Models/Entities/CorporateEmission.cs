using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero.Models.Entities
{
    public class CorporateEmission
    {
        public int Id { get; set; }

        public int CorporateCompanyId { get; set; }
        public CorporateCompany Company { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }

        public double ElectricityKWh { get; set; }
        public double FuelLiters { get; set; }
        public double TransportKm { get; set; }
        public double WasteKg { get; set; }

        public double ElectricityEmissions { get; set; }
        public double FuelEmissions { get; set; }
        public double TransportEmissions { get; set; }
        public double WasteEmissions { get; set; }

        public double TotalEmissions => ElectricityEmissions + FuelEmissions + TransportEmissions + WasteEmissions;
    }
}
