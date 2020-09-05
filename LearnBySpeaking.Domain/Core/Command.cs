using FluentValidation.Results;
using System;

namespace LearnBySpeaking.Domain.Core
{
    public abstract class Command : Message
    {
        protected Command()
        {
        }

        protected Command(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public DateTime Timestamp { get; protected set; }
        public ValidationResult ValidationResult { get; set; }
        public abstract bool IsValid();
    }
}