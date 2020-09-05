using LearnBySpeaking.Domain.Validations.AppParameterValidations;

namespace LearnBySpeaking.Domain.Commands.AppParameter
{
    public class AppParameterUpdateCommand : AppParameterCommand
    {
        public AppParameterUpdateCommand(int id, string name, string value, string description)
        {
            Id = id;
            Name = name;
            Value = value;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = new AppParameterUpdateValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}