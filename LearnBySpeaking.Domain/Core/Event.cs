using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LearnBySpeaking.Domain.Core
{
    public abstract class Event : Message, INotification
    {
        // this method is just for DomainNotification class
        protected Event()
        {
        }

        protected Event(IUser user)
        {
            MongoId = ObjectId.GenerateNewId().ToString();
            Timestamp = DateTime.Now;
            User = user.Name;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; }

        [BsonElement(nameof(EventType))]
        public string EventType { get; set; }

        [BsonElement(nameof(TableName))]
        public string TableName { get; set; }

        [BsonElement(nameof(Timestamp))]
        public DateTime Timestamp { get; protected set; }

        [BsonElement(nameof(User))]
        public string User { get; protected set; }

        public int Id { get; set; }
    }
}