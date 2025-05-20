using FluentValidation;
using ManagementSystem.BLL.DTO.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules.CategoryValidations
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequestDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Geçersiz Id");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Kategori adı boş olamaz");
        }
    }
}
