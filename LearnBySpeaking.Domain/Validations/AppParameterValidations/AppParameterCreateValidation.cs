using LearnBySpeaking.Domain.Commands.AppParameter;

namespace LearnBySpeaking.Domain.Validations.AppParameterValidations
{
    public class AppParameterCreateValidation : AppParameterValidation<AppParameterCreateCommand>
    {
        public AppParameterCreateValidation()
        {
            ValidateName();
            ValidateValue();
            ValidateDescription();
        }
    }
}