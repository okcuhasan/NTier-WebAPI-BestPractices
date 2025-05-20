using FluentValidation;
using ManagementSystem.BLL.DTO.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.ValidationRules.ProductValidations
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequestDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).WithMessage("Geçersiz Id");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı boş olamaz");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır");
            RuleFor(p => p.Stock).GreaterThanOrEqualTo(0).WithMessage("Stok değeri negatif olamaz");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Kategori seçilmelidir");
        }
    }
}
