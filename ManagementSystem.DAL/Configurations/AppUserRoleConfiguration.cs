using ManagementSystem.ENTITIES.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Configurations
{
    public class AppUserRoleConfiguration : BaseConfiguration<AppUserRole>
    {
        public override void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            base.Configure(builder);

            builder.Ignore(au => au.Id);

            /* ! automattically composite key its because of IdentityUserRole inheritance ! */
            //builder.HasKey(au => new
            //{
            //    au.UserId,
            //    au.RoleId
            //});
        }
    }
}
