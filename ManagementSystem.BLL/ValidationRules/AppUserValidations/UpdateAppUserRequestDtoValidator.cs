using FluentValidation;
using ManagementSystem.BLL.DTO.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules.AppUserValidations
{
    public class UpdateAppUserRequestDtoValidator : AbstractValidator<UpdateAppUserRequestDto>
    {
        public UpdateAppUserRequestDtoValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz")
            .EmailAddress().WithMessage("Geçerli bir email giriniz");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz");
        }
    }
}
