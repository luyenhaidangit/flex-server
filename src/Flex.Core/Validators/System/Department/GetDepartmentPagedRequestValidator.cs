using Flex.Core.Models.System.Department;
using FluentValidation;

namespace Flex.Core.Validators.System.Department
{
    public class GetDepartmentPagedRequestValidator : BasePagedRequestValidator<GetDepartmentPagedRequest>
    {
        public GetDepartmentPagedRequestValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0)
                .WithMessage("PageIndex phải lớn hơn 0.");
        }
    }
}
