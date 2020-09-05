using LearnBySpeaking.Domain.Events;
using LearnBySpeaking.Domain.Interfaces.Core;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.EventHandlers
{
    public class AppParameterEventHandler : INotificationHandler<AppParameterCreatedEvent>,
        INotificationHandler<AppParameterUpdatedEvent>,
        INotificationHandler<AppParameterRemovedEvent>
    {
        private readonly IMongoEventRepository _mongoEventRepository;

        public AppParameterEventHandler(IMongoEventRepository mongoEventRepository)
        {
            _mongoEventRepository = mongoEventRepository;
        }

        public Task Handle(AppParameterCreatedEvent notification, CancellationToken cancellationToken)
        {
            _mongoEventRepository.Create<AppParameterCreatedEvent>(notification);
            return Task.CompletedTask;
        }

        public Task Handle(AppParameterUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _mongoEventRepository.Create<AppParameterUpdatedEvent>(notification);
            return Task.CompletedTask;
        }

        public Task Handle(AppParameterRemovedEvent notification, CancellationToken cancellationToken)
        {
            _mongoEventRepository.Create<AppParameterRemovedEvent>(notification);
            return Task.CompletedTask;
        }
    }
}