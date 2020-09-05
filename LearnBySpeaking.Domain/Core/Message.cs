using MediatR;

namespace LearnBySpeaking.Domain.Core
{
    public abstract class Message : IRequest, IRequest<Unit>, IBaseRequest
    {
        protected Message()
        {
        }

        public string MessageType { get; protected set; }
        public int AggregateId { get; protected set; }
    }
}