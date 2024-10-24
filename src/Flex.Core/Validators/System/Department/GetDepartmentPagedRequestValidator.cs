using Flex.Core.Models.System.Department;
using FluentValidation;

namespace Flex.Core.Validators.System.Department
{
    public class GetDepartmentPagedRequestValidator : BasePagedRequestValidator<GetDepartmentPagedRequest>
    {
        protected override Dictionary<string, string> OrderByMappings => new()
        {
            //{ "name", "CustName" }
        };

        public GetDepartmentPagedRequestValidator()
        {
            RuleFor(x => x.Name)
            .Custom((name, context) =>
            {
                if (name != null)
                {
                    context.InstanceToValidate.Name = name.Trim().ToLower();
                }
            });
        }
    }
}
