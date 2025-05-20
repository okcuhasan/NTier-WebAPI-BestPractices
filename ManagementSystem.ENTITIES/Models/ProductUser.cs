using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.ENTITIES.Models
{
    public class ProductUser : BaseEntity
    {
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
