using LearnBySpeaking.Domain.Commands.AppParameter;

namespace LearnBySpeaking.Domain.Validations.AppParameterValidations
{
    public class AppParameterRemoveValidation : AppParameterValidation<AppParameterRemoveCommand>
    {
        public AppParameterRemoveValidation()
        {
            ValidateId();
        }
    }
}