using LearnBySpeaking.Domain.Commands.AppParameter;
using FluentValidation;

namespace LearnBySpeaking.Domain.Validations.AppParameterValidations
{
    public abstract class AppParameterValidation<T> : AbstractValidator<T>
        where T : AppParameterCommand
    {
        public AppParameterValidation()
        {
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name boş bırakılamaz.")
                .Length(2, 150).WithMessage("Name uzunluğu 150 karakterden fazla olamaz");
        }

        protected void ValidateValue()
        {
            RuleFor(c => c.Value)
                .NotEmpty().WithMessage("Value boş bırakılamaz.")
                .Length(1, 150).WithMessage("Value uzunluğu 150 karakterden fazla olamaz");
        }

        protected void ValidateDescription()
        {
            RuleFor(c => c.Description)
                .Length(0, 250).WithMessage("descrption uzunluğu 150 karakterden fazla olamaz");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id must not empty");
        }
    }
}