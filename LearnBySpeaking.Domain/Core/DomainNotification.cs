using System;

namespace LearnBySpeaking.Domain.Core
{
    public class DomainNotification : Event
    {
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public Guid DomainNotificationId { get; }
        public string Key { get; }
        public string Value { get; }
        public int Version { get; }
    }
}