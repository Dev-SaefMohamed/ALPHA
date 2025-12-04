using Hero.Models.Entities;
using Hero.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hero.Data.Configurations;
using System.Linq;

namespace Hero.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =======================
        // DbSets for Corporate Module
        // =======================
        public DbSet<CorporateEmissionCoefficient> CorporateEmissionCoefficients { get; set; }
        public DbSet<CorporateCompany> CorporateCompanies { get; set; }
        public DbSet<CorporateEmission> CorporateEmissions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // =======================
            // Apply existing configurations
            // =======================
            builder.ApplyConfiguration(new ApplicationUserConfiguration());

            // =======================
            // Corporate configurations
            // =======================
            builder.ApplyConfiguration(new CorporateCompanyConfiguration());
            builder.ApplyConfiguration(new CorporateEmissionConfiguration());
            builder.ApplyConfiguration(new CorporateEmissionCoefficientConfiguration());

            // =======================
            // One-to-one relation between ApplicationUser and CorporateCompany
            // Each ApplicationUser can have one CorporateCompany
            // Each CorporateCompany must be linked to one ApplicationUser
            // FK is CorporateCompany.UserId
            // =======================
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.CorporateCompany)
                .WithOne(c => c.User)
                .HasForeignKey<CorporateCompany>(c => c.UserId);

            // =======================
            // Disable cascade delete globally
            // (so deleting a user won't delete related companies/emissions automatically)
            // =======================
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // =======================
            // Seed Data for CorporateEmissionCoefficients (Egypt-specific)
            // =======================
            builder.Entity<CorporateEmissionCoefficient>().HasData(
                new CorporateEmissionCoefficient
                {
                    Id = 1,
                    Category = CategoryType.Electricity,
                    Coefficient = 0.55,
                    Unit = "kWh",
                    Source = "IEA/Climatiq Egypt estimates 2025",
                    LastUpdated = new DateTime(2025, 1, 1),
                    DataQuality = DataQuality.High
                },
                new CorporateEmissionCoefficient
                {
                    Id = 2,
                    Category = CategoryType.Fuel,
                    Coefficient = 2.3,
                    Unit = "Liter",
                    Source = "IEA default (est.)",
                    LastUpdated = new DateTime(2025, 1, 1),
                    DataQuality = DataQuality.Medium
                },
                new CorporateEmissionCoefficient
                {
                    Id = 3,
                    Category = CategoryType.Transportation,
                    TransportationType = TransportationType.Car,
                    Coefficient = 0.21,
                    Unit = "km",
                    Source = "Cairo University study 2023",
                    LastUpdated = new DateTime(2025, 1, 1),
                    DataQuality = DataQuality.High
                },
                new CorporateEmissionCoefficient
                {
                    Id = 4,
                    Category = CategoryType.Transportation,
                    TransportationType = TransportationType.Bus,
                    Coefficient = 0.11,
                    Unit = "km",
                    Source = "Cairo University study 2023",
                    LastUpdated = new DateTime(2025, 1, 1),
                    DataQuality = DataQuality.High
                },
                new CorporateEmissionCoefficient
                {
                    Id = 5,
                    Category = CategoryType.Plastic,
                    Coefficient = 6.0,
                    Unit = "kg",
                    Source = "Global studies (worst-case)",
                    LastUpdated = new DateTime(2025, 1, 1),
                    DataQuality = DataQuality.Estimated
                });
        }
    }
}
