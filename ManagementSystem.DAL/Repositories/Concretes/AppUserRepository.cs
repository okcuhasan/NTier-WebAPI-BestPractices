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
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager; 
        public AppUserRepository(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CreateUserAsync(AppUser user, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if(result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _userManager.Users.Where(au => au.Status != ENTITIES.Enums.DataStatus.Deleted).ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(AppUser user)
        {
            IdentityResult result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(AppUser user)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<AppUser> GetUserAsync(int id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(au => au.Id == id);
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
            if(result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<AppUser> GetUserByNameAsync(string name)
        {
            AppUser user = await _userManager.FindByNameAsync(name);
            return user;
        }

        public async Task<IList<string>> GetUserRolesAsync(string userName)
        {
            AppUser user = await GetUserByNameAsync(userName);
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            return userRoles;
        }

        public async Task<bool> ChangePasswordAsync(string userName, string oldPassword, string newPassword)
        {
            AppUser user = await GetUserByNameAsync(userName);
            IdentityResult result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

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
