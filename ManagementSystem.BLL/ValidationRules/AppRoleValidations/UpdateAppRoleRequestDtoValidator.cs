using FluentValidation;
using ManagementSystem.BLL.DTO.AppRoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules.AppRoleValidations
{
    public class UpdateAppRoleRequestDtoValidator : AbstractValidator<UpdateAppRoleRequestDto>
    {
        public UpdateAppRoleRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Rol adı boş olamaz");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Açıklama en fazla 250 karakter olabilir");
        }
    }
}
