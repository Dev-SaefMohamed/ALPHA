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
    public class CorporateEmissionCoefficientConfiguration : IEntityTypeConfiguration<CorporateEmissionCoefficient>
    {

        public void Configure(EntityTypeBuilder<CorporateEmissionCoefficient> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Category).IsRequired();
            builder.Property(c => c.Coefficient).IsRequired();
            builder.Property(c => c.Unit).HasMaxLength(100);
            builder.Property(c => c.Source).HasMaxLength(200);
            builder.Property(c => c.DataQuality).IsRequired();
        }

    }
}
