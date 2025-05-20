using FluentValidation;
using ManagementSystem.BLL.DTO.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules.AppUserValidations
{
    public class CreateAppUserLoginRequestDtoValidator : AbstractValidator<CreateAppUserLoginRequestDto>
    {
        public CreateAppUserLoginRequestDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz");
        }
    }
}
