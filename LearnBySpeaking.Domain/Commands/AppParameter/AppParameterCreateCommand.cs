using LearnBySpeaking.Domain.Validations.AppParameterValidations;

namespace LearnBySpeaking.Domain.Commands.AppParameter
{
    public class AppParameterCreateCommand : AppParameterCommand
    {
        public AppParameterCreateCommand(string name, string value, string description)
        {
            Name = name;
            Value = value;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = new AppParameterCreateValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}