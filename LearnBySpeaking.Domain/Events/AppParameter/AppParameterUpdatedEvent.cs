using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.Models;

namespace LearnBySpeaking.Domain.Events
{
    public class AppParameterUpdatedEvent : AppParameterEventModel
    {
        public AppParameterUpdatedEvent(AppParameter appParameter, IUser user)
            : base(user)
        {
            AggregateId = appParameter.Id;
            Id = appParameter.Id;
            EventType = "Update";

            Name = appParameter.Name;
            Value = appParameter.Value;
            Description = appParameter.Description;
        }
    }
}