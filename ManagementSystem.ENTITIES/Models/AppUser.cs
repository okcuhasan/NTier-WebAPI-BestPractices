using ManagementSystem.ENTITIES.Enums;
using ManagementSystem.ENTITIES.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.ENTITIES.Models
{
    public class AppUser : IdentityUser<int>, IEntity
    {
        public AppUser()
        {
            CreatedDate = DateTime.Now;
            Status = DataStatus.Inserted;
        }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<AppUserRole> AppUserRoles { get; set; }
        public virtual List<ProductUser> ProductUsers { get; set; }
    }
}
