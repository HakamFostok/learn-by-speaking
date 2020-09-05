using LearnBySpeaking.Domain.Commands.AppParameter;
using FluentValidation;

namespace LearnBySpeaking.Domain.Validations.AppParameterValidations
{
    public class AppParameterCreateBundleValidation : AbstractValidator<AppParameterCreateBundleCommand>
    {
        public AppParameterCreateBundleValidation()
        {
        }
    }
}