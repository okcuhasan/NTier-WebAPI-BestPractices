using ManagementSystem.ENTITIES.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Configurations
{
    public class AppUserConfiguration : BaseConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);

            builder.HasMany(au => au.AppUserRoles)
                .WithOne(ur => ur.AppUser)
                .HasForeignKey(ur => ur.UserId);

            builder.HasMany(au => au.ProductUsers)
                .WithOne(pu => pu.AppUser)
                .HasForeignKey(pu => pu.AppUserId);
        }
    }
}
