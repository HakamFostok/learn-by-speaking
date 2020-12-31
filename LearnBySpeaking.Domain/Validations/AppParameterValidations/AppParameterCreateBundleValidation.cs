using FluentValidation;
using LearnBySpeaking.Domain.Commands.AppParameter;

namespace LearnBySpeaking.Domain.Validations.AppParameterValidations
{
    public class AppParameterCreateBundleValidation : AbstractValidator<AppParameterCreateBundleCommand>
    {
        public AppParameterCreateBundleValidation()
        {
        }
    }
}