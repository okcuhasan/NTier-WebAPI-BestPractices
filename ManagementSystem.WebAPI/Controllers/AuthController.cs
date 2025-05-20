using ManagementSystem.BLL.DTO.AppUserDtos;
using ManagementSystem.BLL.ManagerServices.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAppUserManager _appUserManager;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAppUserManager appUserManager, ILogger<AuthController> logger)
        {
            _appUserManager = appUserManager;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CreateAppUserLoginRequestDto createAppUserLoginRequestDto)
        {
            try
            {
                _logger.LogInformation("Giriş denemesi alındı. Kullanıcı adı: {UserName}", createAppUserLoginRequestDto.UserName);
                string message = await _appUserManager.LoginAsync(createAppUserLoginRequestDto.UserName, createAppUserLoginRequestDto.Password);
                _logger.LogInformation("Kullanıcı başarılı şekilde giriş yaptı: {UserName}", createAppUserLoginRequestDto.UserName);
                return Ok(message);
            }
            catch(ArgumentException ex)
            {
                _logger.LogWarning(ex, "Geçersiz giriş isteği. Kullanıcı adı: {UserName}, Hata: {HataMesaji}", createAppUserLoginRequestDto.UserName, ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch(ApplicationException ex)
            {
                _logger.LogWarning(ex, "Yetkisiz giriş denemesi. Kullanıcı adı: {UserName}, Hata: {HataMesaji}", createAppUserLoginRequestDto.UserName, ex.Message);
                return Unauthorized(new { Error = ex.Message });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Giriş işlemi sırasında beklenmeyen bir hata oluştu. Kullanıcı adı: {UserName}, Hata: {HataMesaji}", createAppUserLoginRequestDto.UserName, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Sunucuda beklenmeyen bir hata oluştu", Detail = ex.Message });
            }
        }
    }
}
