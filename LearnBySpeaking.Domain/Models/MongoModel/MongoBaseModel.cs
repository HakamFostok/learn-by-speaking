using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LearnBySpeaking.Domain.Models.MongoModel
{
    public abstract class MongoBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Data")]
        public string Data { get; set; }

        [BsonElement("ActionType")]
        public string ActionType { get; set; }

        [BsonElement("ActionDate")]
        public string ActionDate { get; set; }
    }
}