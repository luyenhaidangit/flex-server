using Flex.Core.Models.Identity;
using FluentValidation;

namespace Flex.Core.Validators.Identity
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("Tên người dùng không được để trống!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống!");
        }
    }
}