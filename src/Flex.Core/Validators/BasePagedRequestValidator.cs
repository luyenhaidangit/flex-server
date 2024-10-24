using FluentValidation;
using Flex.Core.Models.Common;

namespace Flex.Core.Validators
{
    public abstract class BasePagedRequestValidator<T> : AbstractValidator<T> where T : PagedRequest
    {
        protected virtual Dictionary<string, string> OrderByMappings => new();

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
               .WithMessage($"OrderBy chỉ được nhận {string.Join(", ", OrderByMappings.Keys)}.")
               .Custom((order, context) =>
               {
                   if (order != null && OrderByMappings.TryGetValue(order.Trim().ToLower(), out var mappedValue))
                   {
                       context.InstanceToValidate.OrderBy = mappedValue;
                   }
               });

            RuleFor(x => x.SortBy)
                .Must(order => IsValidSort(order))
                .WithMessage("SortBy chỉ được nhận 'asc' hoặc 'desc'.");
        }

        private bool IsValidSort(string? order)
        {
            if (string.IsNullOrWhiteSpace(order)) return true;
            return order.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                   order.Equals("desc", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsValidOrder(string? order)
        {
            if ((string.IsNullOrWhiteSpace(order)) || (OrderByMappings.Count == 0)) return true;
            return OrderByMappings.ContainsKey(order.Trim().ToLower());
        }
    }
}
