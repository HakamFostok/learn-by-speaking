using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.Interfaces.Core;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.CommandHandler.Core
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediator bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task StartTransactionAsync()
        {
            await _uow.StartTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _uow.RollbackTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _uow.CommitTransactionAsync();
        }

        public async Task<bool> CommitAsync()
        {
            if (_notifications.HasNotifications())
                return false;

            if (await _uow.CommitAsync())
                return true;

            await _bus.Send(new DomainNotification("Commit", "We had a problem during saving your data."));

            return false;
        }

        protected void ThrowIfMessageNotValid(Command command)
        {
            if (command.IsValid())
                return;

            throw new ValidationException(command.ValidationResult.Errors);
        }

        protected void ThrowValidationException(string message)
        {
            List<ValidationFailure> failures = new List<ValidationFailure>()
            {
                new ValidationFailure("Exception", message)
            };

            throw new ValidationException(failures);
        }
    }
}