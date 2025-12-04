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
    public class CorporateCompanyConfiguration : IEntityTypeConfiguration<CorporateCompany>
    {

        public void Configure(EntityTypeBuilder<CorporateCompany> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Email).HasMaxLength(200);
            builder.Property(c => c.Industry).HasMaxLength(100);

            // Relation: One Company has many Emissions
            builder.HasMany(c => c.Emissions)
                   .WithOne(e => e.Company)
                   .HasForeignKey(e => e.CorporateCompanyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
