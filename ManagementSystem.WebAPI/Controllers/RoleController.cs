using AutoMapper;
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
    public class RoleController : ControllerBase
    {
        private readonly IAppRoleManager _appRoleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleController> _logger;
        public RoleController(IAppRoleManager appRoleManager,IMapper mapper, ILogger<RoleController> logger)
        {
            _appRoleManager = appRoleManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("get-roles")]
        public async Task<IActionResult> GetRoles()
        {
            List<AppRole> roles = await _appRoleManager.GetRolesAsync();
            List<AppRoleResponseDto> dtos = _mapper.Map<List<AppRoleResponseDto>>(roles);

            return Ok(dtos);
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(CreateAppRoleRequestDto createAppRoleRequestDto)
        {
            try
            {
                AppRole role = _mapper.Map<AppRole>(createAppRoleRequestDto);
                string message = await _appRoleManager.CreateRoleAsync(role);
                _logger.LogInformation("Rol başarıyla oluşturuldu. Rol adı: {RolAdi}", role.Name);

                return Ok(message);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Rol oluşturulurken eksik veri gönderildi. Hata: {HataMesaji}", ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Aynı isimde bir rol zaten mevcut. Rol adı: {RolAdi}, Hata: {HataMesaji}", createAppRoleRequestDto.Name, ex.Message);
                return Conflict(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rol oluşturulurken beklenmeyen bir hata oluştu. Hata: {HataMesaji}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Rol oluşturulurken beklenmeyen bir hata oluştu", Detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, UpdateAppRoleRequestDto updateAppRoleRequestDto)
        {
            try
            {
                AppRole role = await _appRoleManager.GetRoleAsync(id);
                _mapper.Map(updateAppRoleRequestDto, role);
                string message = await _appRoleManager.UpdateRoleAsync(role);
                _logger.LogInformation("Rol başarıyla güncellendi. ID: {RolId}, Yeni Ad: {RolAdi}", id, role.Name);

                return Ok(message);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Rol güncellenirken eksik veri gönderildi. ID: {RolId}, Hata: {HataMesaji}", id, ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Rol güncelleme çakışması yaşandı. ID: {RolId}, Hata: {HataMesaji}", id, ex.Message);
                return Conflict(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rol güncellenirken beklenmeyen bir hata oluştu. ID: {RolId}, Hata: {HataMesaji}", id, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Rol güncellenirken bir hata oluştu", Detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                AppRole role = await _appRoleManager.GetRoleAsync(id);
                string message = await _appRoleManager.DeleteRoleAsync(role);
                _logger.LogInformation("Rol başarıyla silindi. ID: {RolId}, Ad: {RolAdi}", id, role.Name);

                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Geçersiz rol silme isteği. ID: {RolId}, Hata: {HataMesaji}", id, ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rol silinirken beklenmeyen bir hata oluştu. ID: {RolId}, Hata: {HataMesaji}", id, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Rol silinirken beklenmeyen bir hata oluştu", Detail = ex.Message });
            }
        }
    }
}
