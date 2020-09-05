using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.Core
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> List { get; } = new List<DomainNotification>();

        public List<DomainNotification> GetNotifications()
        {
            return List;
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }
    }

    //public abstract class DomainNotificationHandler : IBtNotificationHandler
    //{
    //    public IList<object> List { get; } = new List<object>();
    //    public bool HasNotifications => List.Any();

    //    public bool Includes(DomainNotification error)
    //    {
    //        return List.Contains(error);
    //    }
    //    public void Add(DomainNotification description)
    //    {
    //        List.Add(description);
    //    }
    //}
}