using AutoMapper;
using ManagementSystem.BLL.DTO.AppUserDtos;
using ManagementSystem.BLL.ManagerServices.Abstracts;
using ManagementSystem.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        public UserController(IAppUserManager appUserManager, IMapper mapper, ILogger<UserController> logger)
        {
            _appUserManager = appUserManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("get-users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            List<AppUser> users = await _appUserManager.GetUsersAsync();
            List<AppUserResponseDto> dtos = _mapper.Map<List<AppUserResponseDto>>(users);

            return Ok(dtos);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateAppUserRequestDto createAppUserRequestDto)
        {
            try
            {
                _logger.LogInformation("Kullanıcı kayıt isteği alındı. Kullanıcı adı: {UserName}", createAppUserRequestDto.UserName);
                AppUser user = _mapper.Map<AppUser>(createAppUserRequestDto);
                string message = await _appUserManager.CreateUserAsync(user, createAppUserRequestDto.Password);
                _logger.LogInformation("Kullanıcı kaydı başarı ile yapıldı. Kullanıcı: {UserName}", user.UserName);

                return Ok(new { Message = message });
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Kullanıcı oluşturulurken boş veri alındı: {HataMesaji}", ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Geçersiz veri ile kullanıcı oluşturulmaya çalışıldı: {HataMesaji}", ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı oluşturulurken beklenmeyen bir hata oluştu: {HataMesaji}", ex.Message);
                return StatusCode(500, new { Error = "Kullanıcı oluşturulurken hata oluştu", Detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, UpdateAppUserRequestDto updateAppUserRequestDto)
        {
            try
            {
                AppUser user = await _appUserManager.GetUserAsync(id);
                _mapper.Map(updateAppUserRequestDto, user);
                string message = await _appUserManager.UpdateUserAsync(user);
                _logger.LogInformation("Kullanıcı başarıyla güncellendi. Id: {Id}", id);

                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Kullanıcı güncellenemedi, geçersiz veri. ID: {Id}, Hata: {HataMesaji}", id, ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Güncellenmek istenen kullanıcı bulunamadı. ID: {Id}, Hata: {HataMesaji}", id, ex.Message);
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı güncellenirken beklenmeyen bir hata oluştu. ID: {Id}, Hata: {HataMesaji}", id, ex.Message);
                return StatusCode(500, new { Error = "Kullanıcı güncellenirken hata oluştu", Detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                AppUser user = await _appUserManager.GetUserAsync(id);
                string message = await _appUserManager.DeleteUserAsync(user);
                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Kullanıcı silinemedi geçersiz ID: {Id}, Hata: {HataMesaji}", id, ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Silinmek istenen kullanıcı bulunamadı. ID: {Id}, Hata: {HataMesaji}", id, ex.Message);
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı silinirken beklenmeyen bir hata oluştu. ID: {Id}, Hata: {HataMesaji}", id, ex.Message);
                return StatusCode(500, new { Error = "Kullanıcı silinirken hata oluştu", Detail = ex.Message });
            }
        }

        [HttpPost("password-change")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto changePasswordRequestDto)
        {
            try
            {
                _logger.LogInformation("Şifre değiştirme isteği alındı. Kullanıcı: {UserName}", changePasswordRequestDto.UserName);
                string message = await _appUserManager.ChangePasswordAsync(
                    changePasswordRequestDto.UserName,
                    changePasswordRequestDto.OldPassword,
                    changePasswordRequestDto.NewPassword
                );
                _logger.LogInformation("Şifre başarı ile değiştirildi. Kullanıcı: {UserName}", changePasswordRequestDto.UserName);
                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Geçersiz veri ile şifre değiştirme denemesi. Kullanıcı: {UserName}, Hata: {HataMesaji}", changePasswordRequestDto.UserName, ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Şifre değiştirme işlemi başarısız, geçersiz veriler girildi. Kullanıcı: {UserName}, Hata: {HataMesaji}", changePasswordRequestDto.UserName, ex.Message);
                return Unauthorized(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şifre değiştirme sırasında beklenmeyen bir hata oluştu. Kullanıcı: {UserName}, Hata: {HataMesaji}", changePasswordRequestDto.UserName, ex.Message);
                return StatusCode(500, new { Error = "Şifre değiştirme işlemi sırasında hata oluştu", Detail = ex.Message });
            }
        }
    }
}
