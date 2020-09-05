using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace LearnBySpeaking.Domain.Events
{
    public class AppParameterEventModel : Event
    {
        public AppParameterEventModel(IUser user)
            : base(user)
        {
            TableName = nameof(AppParameter);
        }

        [BsonElement(nameof(Name))] public string Name { get; set; }

        [BsonElement(nameof(Value))] public string Value { get; set; }

        [BsonElement(nameof(Description))] public string Description { get; set; }
    }
}