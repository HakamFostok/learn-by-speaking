using LearnBySpeaking.Domain.Core;

namespace LearnBySpeaking.Domain.Commands.AppParameter
{
    public abstract class AppParameterCommand : Command
    {
        public string Name { get; protected set; }
        public string Value { get; protected set; }
        public string Description { get; protected set; }
    }
}