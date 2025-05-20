using ManagementSystem.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DAL.Repositories.Abstracts
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductWithCategories();
    }
}
