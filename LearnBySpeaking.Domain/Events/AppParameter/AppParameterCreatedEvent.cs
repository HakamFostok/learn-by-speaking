using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.Models;

namespace LearnBySpeaking.Domain.Events
{
    public class AppParameterCreatedEvent : AppParameterEventModel
    {
        public AppParameterCreatedEvent(AppParameter appParameter, IUser user)
            : base(user)
        {
            AggregateId = appParameter.Id;
            Id = appParameter.Id;
            EventType = "Create";

            Name = appParameter.Name;
            Value = appParameter.Value;
            Description = appParameter.Description;
        }
    }
}