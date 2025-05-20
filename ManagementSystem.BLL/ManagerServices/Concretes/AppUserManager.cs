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
    public class AppUserManager : BaseManager<AppUser>, IAppUserManager
    {
        private readonly IAppUserRepository _appUserRepository;
        public AppUserManager(IAppUserRepository appUserRepository) : base(appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<string> CreateUserAsync(AppUser user, string password)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), "Kullanıcı bilgisi boş olamaz");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Parola boş olamaz", nameof(password));
            }

            bool result = await _appUserRepository.CreateUserAsync(user, password);
            if (!result)
            {
                throw new Exception("Kullanıcı kaydı sırasında beklenmeyen bir hata oluştu");
            }

            return "Kullanıcı başarı ile kaydedildi";
        }

        public async Task<string> DeleteUserAsync(AppUser user)
        {
            if(user == null || user.Id == 0)
            {
                throw new ArgumentException("Silinecek kullanıcı bilgisi eksik");
            }

            bool result = await _appUserRepository.DeleteUserAsync(user);
            if (!result)
            {
                throw new Exception("Kullanıcı silme işlemi başarısız");
            }

            return "Kullanıcı başarı ile silindi";
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _appUserRepository.GetUsersAsync();
        }

        public async Task<string> UpdateUserAsync(AppUser user)
        {
            if(user == null || user.Id == 0)
            {
                throw new ArgumentException("Güncellenecek kullanıcı bilgisi eksik");
            }

            bool result = await _appUserRepository.UpdateUserAsync(user);
            if (!result)
            {
                throw new Exception("Kullanıcı güncellenemedi");
            }

            return "Kullanıcı başarı ile güncellendi";
        }

        public async Task<AppUser> GetUserAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Geçersiz kullanıcı ID");
            }

            var user = await _appUserRepository.GetUserAsync(id);
            if (user == null)
            {
                throw new ApplicationException("Kullanıcı bulunamadı");
            }

            return user;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Kullanıcı adı veya şifre boş olamaz");
            }

            bool result = await _appUserRepository.LoginAsync(userName, password);

            if (!result)
            {
                throw new ApplicationException("Giriş bilgileri hatalı");
            }

            return "Giriş başarılı";
        }

        public async Task<AppUser> GetUserByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Kullanıcı adı boş olamaz");
            }

            var user = await _appUserRepository.GetUserByNameAsync(name);
            if (user == null)
            {
                throw new ApplicationException("Kullanıcı bulunamadı");
            }

            return user;
        }

        public async Task<IList<string>> GetUserRolesAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("Kullanıcı adı boş olamaz");
            }

            IList<string> roles = await _appUserRepository.GetUserRolesAsync(userName);
            return roles;
        }

        public async Task<string> ChangePasswordAsync(string userName, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Kullanıcı adı, eski şifre veya yeni şifre boş olamaz");

            bool result = await _appUserRepository.ChangePasswordAsync(userName, oldPassword, newPassword);

            if (!result)
                throw new ApplicationException("Şifre değiştirme başarısız");

            return "Şifre başarı ile değiştirildi";
        }
    }
}
