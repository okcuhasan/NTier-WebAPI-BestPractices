using FluentValidation;
using ManagementSystem.BLL.DTO.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules.CategoryValidations
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequestDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Kategori adı boş olamaz");
        }
    }
}
