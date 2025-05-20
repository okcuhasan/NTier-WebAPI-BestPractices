using ManagementSystem.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Repositories.Abstracts
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<bool> CreateUserAsync(AppUser user, string password);
        Task<List<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserAsync(int id);
        Task<AppUser> GetUserByNameAsync(string name);
        Task<IList<string>> GetUserRolesAsync(string userName);
        Task<bool> UpdateUserAsync(AppUser user);
        Task<bool> DeleteUserAsync(AppUser user);

        Task<bool> LoginAsync(string userName, string password);
        Task<bool> ChangePasswordAsync(string userName, string oldPassword, string newPassword);
    }
}
