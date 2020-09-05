using LearnBySpeaking.Domain.Core;

namespace LearnBySpeaking.Domain.Events
{
    public class AppParameterRemovedEvent : AppParameterEventModel
    {
        public AppParameterRemovedEvent(int id, IUser user)
            : base(user)
        {
            AggregateId = id;
            Id = id;
            EventType = "Remove";
        }
    }
}