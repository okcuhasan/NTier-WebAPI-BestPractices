using ManagementSystem.DAL.Context;
using ManagementSystem.DAL.Repositories.Abstracts;
using ManagementSystem.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Repositories.Concretes
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductWithCategories()
        {
            List<Product> entities = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return entities;
        }
    }
}
