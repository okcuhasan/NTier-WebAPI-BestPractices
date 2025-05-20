using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.DTO.AppUserDtos
{
    public class CreateAppUserLoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
