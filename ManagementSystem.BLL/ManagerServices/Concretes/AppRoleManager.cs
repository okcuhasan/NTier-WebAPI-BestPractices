using ManagementSystem.BLL.ManagerServices.Abstracts;
using ManagementSystem.DAL.Repositories.Abstracts;
using ManagementSystem.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ManagerServices.Concretes
{
    public class AppRoleManager : BaseManager<AppRole>, IAppRoleManager
    {
        private readonly IAppRoleRepository _appRoleRepository;
        public AppRoleManager(IAppRoleRepository appRoleRepository) : base(appRoleRepository)
        {
            _appRoleRepository = appRoleRepository;
        }

        public async Task<string> CreateRoleAsync(AppRole role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role), "Rol bilgisi boş olamaz");

            var roles = await _appRoleRepository.GetRolesAsync();
            if (roles.Any(r => r.Name == role.Name))
                throw new ApplicationException("Aynı isimde bir rol zaten mevcut");

            bool result = await _appRoleRepository.CreateRoleAsync(role);
            if (!result)
                throw new Exception("Rol eklenirken beklenmeyen bir hata oluştu");

            return "Rol başarı ile eklendi";
        }

        public async Task<AppRole> GetRoleAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Geçersiz rol ID");

            var role = await _appRoleRepository.GetRoleAsync(id);
            if (role == null)
                throw new ApplicationException("Rol bulunamadı");

            return role;
        }

        public async Task<List<AppRole>> GetRolesAsync()
        {
            return await _appRoleRepository.GetRolesAsync();
        }

        public async Task<string> UpdateRoleAsync(AppRole role)
        {
            if (role == null || role.Id == 0)
                throw new ArgumentException("Güncellenecek rol bilgisi geçersiz");

            var existingRoles = await _appRoleRepository.GetRolesAsync();
            if (existingRoles.Any(r => r.Name == role.Name && r.Id != role.Id))
                throw new ApplicationException("Aynı isimde başka bir rol zaten mevcut");

            bool result = await _appRoleRepository.UpdateRoleAsync(role);
            if (!result)
                throw new Exception("Rol güncellenemedi");

            return "Rol başarı ile güncellendi";
        }

        public async Task<string> DeleteRoleAsync(AppRole role)
        {
            if (role == null || role.Id == 0)
                throw new ArgumentException("Silinecek rol bilgisi geçersiz");

            bool result = await _appRoleRepository.DeleteRoleAsync(role);
            if (!result)
                throw new Exception("Rol silinemedi");

            return "Rol başarı ile silindi";
        }

        public async Task<string> AssignRoleToUserAsync(AppUser user, string roleName)
        {
            if (user == null || user.Id == 0)
                throw new ArgumentException("Kullanıcı bilgisi geçersiz");

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Rol adı boş olamaz");

            bool result = await _appRoleRepository.AssignRoleToUserAsync(user, roleName);
            if (!result)
                throw new Exception("Rol ataması başarısız oldu");

            return "Rol kullanıcıya başarı ile atandı";
        }
    }
}
