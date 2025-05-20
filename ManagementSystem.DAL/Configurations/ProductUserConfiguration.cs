using ManagementSystem.ENTITIES.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Configurations
{
    public class ProductUserConfiguration : BaseConfiguration<ProductUser>
    {
        public override void Configure(EntityTypeBuilder<ProductUser> builder)
        {
            base.Configure(builder);
        }
    }
}
