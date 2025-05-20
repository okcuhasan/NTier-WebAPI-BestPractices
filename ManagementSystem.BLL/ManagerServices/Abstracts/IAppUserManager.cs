using ManagementSystem.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ManagerServices.Abstracts
{
    public interface IAppUserManager : IManager<AppUser>
    {
        Task<string> CreateUserAsync(AppUser user, string password);
        Task<List<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserAsync(int id);
        Task<AppUser> GetUserByNameAsync(string name);
        Task<IList<string>> GetUserRolesAsync(string userName);
        Task<string> UpdateUserAsync(AppUser user);
        Task<string> DeleteUserAsync(AppUser user);

        Task<string> LoginAsync(string userName, string password);
        Task<string> ChangePasswordAsync(string userName, string oldPassword, string newPassword);
    }
}
