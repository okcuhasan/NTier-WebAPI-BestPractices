using AutoMapper;
using ManagementSystem.BLL.DTO.ProductDtos;
using ManagementSystem.BLL.ManagerServices.Abstracts;
using ManagementSystem.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text.Json;

namespace ManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductManager _productManager;
        private readonly IDistributedCache _distributedCache;
        public ProductController(IProductManager productManager, IMapper mapper, ILogger<ProductController> logger, IDistributedCache distributedCache)
        {
            _mapper = mapper;
            _logger = logger;
            _distributedCache = distributedCache;
            _productManager = productManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetProducts()
        {
            string cacheKey = "RedisCache:ProductList";
            string cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if(!string.IsNullOrEmpty(cachedData))
            {
                List<ProductResponseDto> cachedDtos = JsonSerializer.Deserialize<List<ProductResponseDto>>(cachedData);
                _logger.LogInformation("Ürün listesi Redis Cache ile çekildi");
                return Ok(cachedDtos);
            }

            List<Product> products = await _productManager.GetAll().ToListAsync();
            List<ProductResponseDto> dtos = _mapper.Map<List<ProductResponseDto>>(products);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                /* When the cache is accessed, the time is extended by 2 more minutes. However, if it is never accessed, it is deleted after 2 minutes. */
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };

            string serialized = JsonSerializer.Serialize(dtos);
            await _distributedCache.SetStringAsync(cacheKey, serialized, options);

            _logger.LogInformation("Ürün listesi Redis Cache'e kaydedildi");

            return Ok(dtos);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(CreateProductRequestDto dto)
        {
            try
            {
                Product product = _mapper.Map<Product>(dto);
                string message = await _productManager.AddAsync(product);

                await _distributedCache.RemoveAsync("RedisCache:ProductList");
                await _distributedCache.RemoveAsync("RedisCache:ProductCategories");

                _logger.LogInformation("Yeni ürün eklendi: {@Product}", product);

                return Ok(message);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Uygulama hatası (ekleme): {Message}", ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün eklenirken beklenmeyen bir hata oluştu");
                return StatusCode(500, new { Message = "Ürün eklenirken bir hata oluştu", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequestDto dto)
        {
            try
            {
                Product product = await _productManager.FirstOrDefaultAsync(p => p.Id == id);
                _mapper.Map(dto, product);
                string message = await _productManager.UpdateAsync(product);

                await _distributedCache.RemoveAsync("RedisCache:ProductList");
                await _distributedCache.RemoveAsync("RedisCache:ProductCategories");

                _logger.LogInformation("Ürün güncellendi: {@Product}", product);

                return Ok(message);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Uygulama hatası (güncelleme): {Message}", ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün güncellenirken beklenmeyen bir hata oluştu");
                return StatusCode(500, new { Message = "Ürün güncellenirken bir hata oluştu", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                Product product = await _productManager.FirstOrDefaultAsync(p => p.Id == id);
                string message = await _productManager.DestroyAsync(product);

                await _distributedCache.RemoveAsync("RedisCache:ProductList");
                await _distributedCache.RemoveAsync("RedisCache:ProductCategories");

                _logger.LogWarning("Ürün silindi - Id: {Id}, Ad: {Name}", product.Id, product.Name);

                return Ok(message);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Uygulama hatası (silme): {Message}", ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün silinirken beklenmeyen bir hata oluştu");
                return StatusCode(500, new { Message = "Ürün silinirken bir hata oluştu", detail = ex.Message });
            }
        }

        [HttpGet("get-product-category")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetProductCategories()
        {
            string cacheKey = "RedisCache:ProductCategories";
            string cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if(!string.IsNullOrEmpty(cachedData))
            {
                List<ProductResponseDto> cachedDtos = JsonSerializer.Deserialize<List<ProductResponseDto>>(cachedData);
                _logger.LogInformation("Kategoriler ve ürünleri Redis Cache ile çekildi");
                return Ok(cachedDtos);
            }

            List<Product> products = await _productManager.GetProductWithCategories();
            List<ProductResponseDto> dtos = products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            }).ToList();

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };

            string serialized = JsonSerializer.Serialize(dtos);
            await _distributedCache.SetStringAsync(cacheKey, serialized, options);

            _logger.LogInformation("Kategoriler ve ürünleri Redis Cache'e kaydedildi");

            return Ok(dtos);
        }
    }
}
