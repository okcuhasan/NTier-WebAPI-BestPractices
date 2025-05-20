using ManagementSystem.DAL.Context;
using ManagementSystem.DAL.Repositories.Abstracts;
using ManagementSystem.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Repositories.Concretes
{
    public class AppRoleRepository : BaseRepository<AppRole>, IAppRoleRepository
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AppRoleRepository(ApplicationDbContext context, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager) : base(context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> CreateRoleAsync(AppRole role)
        {
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<AppRole>> GetRolesAsync()
        {
            List<AppRole> roles = await _roleManager.Roles.Where(ar => ar.Status != ENTITIES.Enums.DataStatus.Deleted).ToListAsync();
            return roles;
        }

        public async Task<bool> UpdateRoleAsync(AppRole appRole)
        {
            IdentityResult result = await _roleManager.UpdateAsync(appRole);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteRoleAsync(AppRole appRole)
        {
            IdentityResult result = await _roleManager.DeleteAsync(appRole);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<AppRole> GetRoleAsync(int id)
        {
            return await _roleManager.Roles.FirstOrDefaultAsync(ar => ar.Id == id);
        }

        public async Task<bool> AssignRoleToUserAsync(AppUser appUser, string roleName)
        {
            IdentityResult result = await _userManager.AddToRoleAsync(appUser, roleName);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
