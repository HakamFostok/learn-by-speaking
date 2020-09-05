using LearnBySpeaking.Domain.Validations.AppParameterValidations;

namespace LearnBySpeaking.Domain.Commands.AppParameter
{
    public class AppParameterRemoveCommand : AppParameterCommand
    {
        public AppParameterRemoveCommand()
        {
        }

        public AppParameterRemoveCommand(int id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new AppParameterRemoveValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}