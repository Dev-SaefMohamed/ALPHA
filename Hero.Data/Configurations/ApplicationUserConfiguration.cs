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
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // the relatioship btw ApplicationUser & Activity
           
            //builder.HasMany(u => u.)
            //       .WithOne(a => a.User)
            //       .HasForeignKey(a => a.UserId)
            //       .OnDelete(DeleteBehavior.Cascade);

            
            // builder.Property(u => u.FullName).HasMaxLength(100);
        }
    }
}
