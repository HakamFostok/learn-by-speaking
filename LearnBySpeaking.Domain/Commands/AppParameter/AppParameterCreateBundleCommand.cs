using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.Validations.AppParameterValidations;

namespace LearnBySpeaking.Domain.Commands.AppParameter
{
    public class AppParameterCreateBundleCommand : Command
    {
        public bool ThrowException { get; set; }

        public AppParameterCreateBundleCommand(bool throwException)
        {
            ThrowException = throwException;
        }

        public override bool IsValid()
        {
            ValidationResult = new AppParameterCreateBundleValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}