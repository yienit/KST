using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace KST.Model.Validator
{
    /// <summary>
    /// Agency实体数据验证类
    /// </summary>
    public class AgencyValidator : AbstractValidator<Agency>
    {
        public AgencyValidator()
        {
            RuleFor(agency => agency.Name).NotNull().NotEmpty().WithMessage(ErrorMessage.NOT_EMPTY_MSG);
            RuleFor(agency => agency.Name).Length(4, 50).WithMessage(ErrorMessage.NOT_LENGTH_MSG);
        }
    }
}
