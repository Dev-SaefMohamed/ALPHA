namespace Hero.Models.ViewModels
{
    public class CorporateEmissionInputViewModel
    {
        public string? CompanyName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        
        public double ElectricityKWh { get; set; }
        public double FuelLiters { get; set; }
        public double TransportKm { get; set; }
        public double WasteKg { get; set; }

        
        public string? UserId { get; set; }

        
        public string? Industry { get; set; }
    }
}
