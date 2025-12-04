using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero.Models.ViewModels
{
    public class CorporateEmissionResultViewModel
    {

        public string CompanyName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public double ElectricityEmissions { get; set; }
        public double FuelEmissions { get; set; }
        public double TransportEmissions { get; set; }
        public double WasteEmissions { get; set; }
        public double TotalEmissions { get; set; }

    }
}
