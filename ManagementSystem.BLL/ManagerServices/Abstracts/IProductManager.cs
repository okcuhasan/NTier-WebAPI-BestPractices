using ManagementSystem.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ManagerServices.Abstracts
{
    public interface IProductManager : IManager<Product>
    {
        Task<List<Product>> GetProductWithCategories();
    }
}
