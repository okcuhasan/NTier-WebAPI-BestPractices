using ManagementSystem.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ManagerServices.Abstracts
{
    public interface IAppRoleManager : IManager<AppRole>
    {
        Task<string> CreateRoleAsync(AppRole role);
        Task<List<AppRole>> GetRolesAsync();
        Task<string> DeleteRoleAsync(AppRole role);
        Task<string> UpdateRoleAsync(AppRole role);
        Task<AppRole> GetRoleAsync(int id);

        Task<string> AssignRoleToUserAsync(AppUser appUser, string roleName);
    }
}
