using ManagementSystem.BLL.DTO.AppRoleDtos;
using ManagementSystem.BLL.ManagerServices.Abstracts;
using ManagementSystem.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAppRoleManager _appRoleManager;
        private readonly IAppUserManager _appUserManager;
        public AdminController(IAppRoleManager appRoleManager, IAppUserManager appUserManager, ILogger<AdminController> logger)
        {
            _logger = logger;
            _appRoleManager = appRoleManager;
            _appUserManager = appUserManager;
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(AssignRoleDto assignRoleDto)
        {
            try
            {
                AppUser user = await _appUserManager.GetUserByNameAsync(assignRoleDto.UserName);
                string message = await _appRoleManager.AssignRoleToUserAsync(user, assignRoleDto.RoleName);
                _logger.LogInformation("Rol başarıyla atandı. Kullanıcı: {UserName}, Rol: {RoleName}", assignRoleDto.UserName, assignRoleDto.RoleName);

                return Ok(message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Rol atama isteğinde geçersiz veri. Kullanıcı: {UserName}, Hata: {HataMesaji}", assignRoleDto.UserName, ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Kullanıcı bulunamadı. Kullanıcı: {UserName}, Hata: {HataMesaji}", assignRoleDto.UserName, ex.Message);
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rol atama sırasında beklenmeyen bir hata oluştu. Kullanıcı: {UserName}, Hata: {HataMesaji}", assignRoleDto.UserName, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Beklenmeyen bir hata oluştu", Detail = ex.Message });
            }
        }

        [HttpGet("user-roles")]
        public async Task<IActionResult> GetUserRoles(string userName)
        {
            try
            {
                IList<string> userRoles = await _appUserManager.GetUserRolesAsync(userName);
                _logger.LogInformation("Kullanıcının rol bilgileri başarıyla getirildi. Kullanıcı: {UserName}, Roller: {@Roller}", userName, userRoles);

                return Ok(userRoles);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Kullanıcı rolleri getirilirken geçersiz kullanıcı adı. Kullanıcı: {UserName}, Hata: {HataMesaji}", userName, ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı rolleri getirilirken beklenmeyen bir hata oluştu. Kullanıcı: {UserName}, Hata: {HataMesaji}", userName, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Roller alınırken bir hata oluştu", Detail = ex.Message });
            }
        }
    }
}
