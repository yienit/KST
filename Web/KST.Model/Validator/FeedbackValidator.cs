using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace KST.Model.Validator
{
    /// <summary>
    /// Feedback实体数据验证类
    /// </summary>
    public class FeedbackValidator : AbstractValidator<FeedbackRecord>
    {
        public FeedbackValidator()
        {
            RuleFor(feedback => feedback.Content).NotNull().NotEmpty().WithMessage(ErrorMessage.NOT_EMPTY_MSG);
            RuleFor(feedback => feedback.Content).Length(0, 500).WithMessage(ErrorMessage.NOT_LENGTH_MSG);

            RuleFor(feedback => feedback.Contact).Length(0, 30).WithMessage(ErrorMessage.NOT_LENGTH_MSG);

            RuleFor(feedback => feedback.TerminalType).Must(type => { return Enum.IsDefined(typeof(TerminalType), type); }).WithMessage(ErrorMessage.NOT_RANGE_MSG);
        }
    }
}
