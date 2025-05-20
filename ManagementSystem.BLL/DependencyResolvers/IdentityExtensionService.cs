using ManagementSystem.DAL.Context;
using ManagementSystem.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.DependencyResolvers
{
    public static class IdentityExtensionService
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(io =>
            {
                io.Password.RequiredUniqueChars = 0;
                io.Password.RequiredLength = 3;
                io.Password.RequireNonAlphanumeric = false;
                io.Password.RequireDigit = false;
                io.Password.RequireLowercase = false;
                io.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}
