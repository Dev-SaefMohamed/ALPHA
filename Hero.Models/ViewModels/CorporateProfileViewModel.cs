using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero.Models.ViewModels
{
    public class CorporateProfileViewModel
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Industry { get; set; }

        public double TotalEmissions { get; set; }
        public int LatestMonth { get; set; }
        public int LatestYear { get; set; }
    }
}
