using Hero.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero.Data.Configurations
{
    public class CorporateEmissionConfiguration : IEntityTypeConfiguration<CorporateEmission>
    {

        public void Configure(EntityTypeBuilder<CorporateEmission> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Year).IsRequired();
            builder.Property(e => e.Month).IsRequired();

            builder.Property(e => e.ElectricityKWh).HasDefaultValue(0);
            builder.Property(e => e.FuelLiters).HasDefaultValue(0);
            builder.Property(e => e.TransportKm).HasDefaultValue(0);
            builder.Property(e => e.WasteKg).HasDefaultValue(0);

            builder.Property(e => e.ElectricityEmissions).HasDefaultValue(0);
            builder.Property(e => e.FuelEmissions).HasDefaultValue(0);
            builder.Property(e => e.TransportEmissions).HasDefaultValue(0);
            builder.Property(e => e.WasteEmissions).HasDefaultValue(0);
        }

    }
}
