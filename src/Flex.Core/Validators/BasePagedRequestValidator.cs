using FluentValidation;
using Flex.Core.Models.Common;

namespace Flex.Core.Validators
{
    public abstract class BasePagedRequestValidator<T> : AbstractValidator<T> where T : PagedRequest
    {
        protected BasePagedRequestValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0)
                .WithMessage("PageIndex phải là số nguyên dương lớn hơn 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("PageSize phải là số nguyên dương lớn hơn 0.");

            RuleFor(x => x.OrderBy)
                .Must(order => IsValidOrder(order))
                .WithMessage("OrderBy chỉ được nhận 'asc' hoặc 'desc'.");
        }

        private bool IsValidOrder(string? order)
        {
            if (string.IsNullOrWhiteSpace(order)) return true;
            return order.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                   order.Equals("desc", StringComparison.OrdinalIgnoreCase);
        }
    }
}
