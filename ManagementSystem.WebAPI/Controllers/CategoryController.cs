using AutoMapper;
using ManagementSystem.BLL.DTO.CategoryDtos;
using ManagementSystem.BLL.ManagerServices.Abstracts;
using ManagementSystem.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryManager _categoryManager;
        private readonly IMemoryCache _memoryCache;
        public CategoryController(ICategoryManager categoryManager, IMapper mapper, ILogger<CategoryController> logger, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _logger = logger;
            _memoryCache = memoryCache;
            _categoryManager = categoryManager;
        }

        [HttpGet("get-categories")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetCategories()
        {
            if(!_memoryCache.TryGetValue("categories", out List<CategoryResponseDto> cachedDtos))
            {
                List<Category> categories = await _categoryManager.GetAll().ToListAsync();
                cachedDtos = _mapper.Map<List<CategoryResponseDto>>(categories);

                _memoryCache.Set("categories", cachedDtos, TimeSpan.FromMinutes(10));
                _logger.LogInformation("Kategori verileri MemoryCache'e yazıldı");
            }
            else
            {
                _logger.LogInformation("Kategori verileri MemoryCache'ten alındı");
            }

            return Ok(cachedDtos);
        }

        [HttpPost("create-category")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto dto)
        {
            try
            {
                Category category = _mapper.Map<Category>(dto);
                string message = await _categoryManager.AddAsync(category);

                // cache clear
                _memoryCache.Remove("categories");

                _logger.LogInformation("Yeni kategori eklendi: {@Category}", category);

                return Ok(message);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Uygulama hatası (ekleme): {Message}", ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori eklenirken beklenmeyen bir hata oluştu");
                return StatusCode(500, new { Message = "Kategori eklenirken bir hata oluştu", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequestDto dto)
        {
            try
            {
                Category category = await _categoryManager.FirstOrDefaultAsync(c => c.Id == id);
                _mapper.Map(dto, category);
                string message = await _categoryManager.UpdateAsync(category);

                _memoryCache.Remove("categories");

                _logger.LogInformation("Kategori güncellendi: {@Category}", category);

                return Ok(message);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Uygulama hatası (güncelleme): {Message}", ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori güncellenirken beklenmeyen bir hata oluştu");
                return StatusCode(500, new { Message = "Kategori güncellenirken bir hata oluştu", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                Category category = await _categoryManager.FirstOrDefaultAsync(c => c.Id == id);
                string message = await _categoryManager.DestroyAsync(category);

                _memoryCache.Remove("categories");

                _logger.LogWarning("Kategori silindi - Id: {Id}, Ad: {Name}", category.Id, category.Name);

                return Ok(message);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Uygulama hatası (silme): {Message}", ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori silinirken beklenmeyen bir hata oluştu");
                return StatusCode(500, new { Message = "Kategori silinirken bir hata oluştu", detail = ex.Message });
            }
        }
    }
}
