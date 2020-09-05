using LearnBySpeaking.Domain.Commands.AppParameter;

namespace LearnBySpeaking.Domain.Validations.AppParameterValidations
{
    public class AppParameterUpdateValidation : AppParameterValidation<AppParameterUpdateCommand>
    {
        public AppParameterUpdateValidation()
        {
            ValidateId();
            ValidateName();
            ValidateValue();
            ValidateDescription();
        }
    }
}