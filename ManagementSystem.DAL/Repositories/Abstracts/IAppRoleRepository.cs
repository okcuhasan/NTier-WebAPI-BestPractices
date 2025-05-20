using ManagementSystem.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Repositories.Abstracts
{
    public interface IAppRoleRepository : IRepository<AppRole>
    {
        Task<bool> CreateRoleAsync(AppRole role);
        Task<List<AppRole>> GetRolesAsync();
        Task<bool> DeleteRoleAsync(AppRole role);
        Task<bool> UpdateRoleAsync(AppRole role);
        Task<AppRole> GetRoleAsync(int id);

        Task<bool> AssignRoleToUserAsync(AppUser appUser, string roleName);
    }
}
