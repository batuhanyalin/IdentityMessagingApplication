using FluentValidation;
using IdentityMessagingApplication.DtoLayer.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessagingApplication.BusinessLayer.ValidationRules.UserValidation
{
    public class MyProfileEditValidation:AbstractValidator<MyProfileUpdateDto>
    {
        public MyProfileEditValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş geçilemez").MinimumLength(3).WithMessage("Ad minimum 3 karakter olmak zorundadır.").MaximumLength(20).WithMessage("Ad maksimum 20 karakter olabilir.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad boş geçilemez").MinimumLength(3).WithMessage("Soyad minimum 3 karakter olmak zorundadır.").MaximumLength(20).WithMessage("Soyad maksimum 20 karakter olabilir.");
            RuleFor(x => x.Profession).NotEmpty().WithMessage("Meslek boş geçilemez").MinimumLength(3).WithMessage("Meslek minimum 3 karakter olmak zorundadır.").MaximumLength(20).WithMessage("Meslek maksimum 20 karakter olabilir.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon boş geçilemez, başında sıfır olmadan bütün rakamları bitişik olarak yazın.").MinimumLength(10).WithMessage("Telefon bilgisi yalnızca 10 hane olabilir, daha kısa olamaz.").MaximumLength(10).WithMessage("Telefon bilgisi yalnızca 10 hane olabilir, daha uzun olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş geçilemez").MinimumLength(10).WithMessage("E-posta minimum 10 karakter olmak zorundadır.").MaximumLength(70).WithMessage("E-posta maksimum 70 karakter olabilir.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş geçilemez").MinimumLength(4).WithMessage("Şehir minimum 4 karakter olmak zorundadır.").MaximumLength(14).WithMessage("Şehir maksimum 14 karakter olabilir.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez").MinimumLength(3).WithMessage("Kullanıcı adı minimum 4 karakter olmak zorundadır.").MaximumLength(12).WithMessage("Kullanıcı adı maksimum 12 karakter olabilir.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi girin.");
        }
    }
}
