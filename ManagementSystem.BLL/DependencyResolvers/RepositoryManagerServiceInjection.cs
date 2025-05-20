using ManagementSystem.BLL.ManagerServices.Abstracts;
using ManagementSystem.BLL.ManagerServices.Concretes;
using ManagementSystem.DAL.Repositories.Abstracts;
using ManagementSystem.DAL.Repositories.Concretes;
using ManagementSystem.DAL.UnitOfWork.Abstracts;
using ManagementSystem.DAL.UnitOfWork.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.DependencyResolvers
{
    public static class RepositoryManagerServiceInjection
    {
        public static IServiceCollection AddRepositoryManagerServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAppRoleRepository, AppRoleRepository>();
            services.AddScoped(typeof(IManager<>), typeof(BaseManager<>));
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<IAppRoleManager, AppRoleManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
