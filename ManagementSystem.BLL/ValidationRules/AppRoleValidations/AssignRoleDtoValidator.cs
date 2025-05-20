using FluentValidation;
using ManagementSystem.BLL.DTO.AppRoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules.AppRoleValidations
{
    public class AssignRoleDtoValidator : AbstractValidator<AssignRoleDto>
    {
        public AssignRoleDtoValidator()
        {
            RuleFor(ar => ar.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz");

            RuleFor(ar => ar.RoleName)
                .NotEmpty().WithMessage("Rol adı boş olamaz");
        }
    }
}
