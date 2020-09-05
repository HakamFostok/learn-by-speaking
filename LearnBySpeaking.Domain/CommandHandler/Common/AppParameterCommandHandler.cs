using LearnBySpeaking.Domain.Commands.AppParameter;
using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.Events;
using LearnBySpeaking.Domain.i18n;
using LearnBySpeaking.Domain.Interfaces.Core;
using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.CommandHandler.Common
{
    public class AppParameterCommandHandler : Core.CommandHandler,
        IRequestHandler<AppParameterCreateCommand>,
        IRequestHandler<AppParameterUpdateCommand>,
        IRequestHandler<AppParameterRemoveCommand>,
        IRequestHandler<AppParameterCreateBundleCommand>
    {
        private readonly IAppParameterRepository _appParameterRepository;
        private readonly IMediator _bus;
        private readonly IUser _user;

        public AppParameterCommandHandler(IAppParameterRepository appParameterRepository,
            IUser user,
            IUnitOfWork uow,
            IMediator bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _appParameterRepository = appParameterRepository;
            _bus = bus;
            _user = user;
        }

        public async Task<Unit> Handle(AppParameterCreateCommand request, CancellationToken cancellationToken)
        {
            //if (!_user.IsAdmin())
            //    throw new ValidationException("You must be admin to execute this method");

            ThrowIfMessageNotValid(request);

            AppParameter appParameter = new AppParameter(request.Name, request.Value, request.Description, _user.Name);

            if (await _appParameterRepository.GetAppParameterByNameAsync(request.Name) != null)
                ThrowValidationException(Lang.RECORD_EXISTS);

            await _appParameterRepository.AddAsync(appParameter);
            await CommitAsync();

            await _bus.Publish(new AppParameterCreatedEvent(appParameter, _user));

            return Unit.Value;
        }

        public async Task<Unit> Handle(AppParameterUpdateCommand request, CancellationToken cancellationToken)
        {
            ThrowIfMessageNotValid(request);

            AppParameter existingAppParameter = await _appParameterRepository.GetAppParameterByNameAsync(request.Name);
            if (existingAppParameter != null && existingAppParameter.Id != request.Id)
                if (request.Name == existingAppParameter.Name)
                    ThrowValidationException(Lang.RECORD_EXISTS);

            AppParameter appParameter = await _appParameterRepository.GetByIdAsync(request.Id);
            appParameter.Update(request.Name, request.Value, request.Description, _user.Name);

            await _appParameterRepository.UpdateAsync(appParameter);
            await CommitAsync();

            await _bus.Publish(new AppParameterUpdatedEvent(appParameter, _user));

            return Unit.Value;
        }

        public async Task<Unit> Handle(AppParameterRemoveCommand request, CancellationToken cancellationToken)
        {
            ThrowIfMessageNotValid(request);

            await _appParameterRepository.RemoveAsync(request.Id);
            await CommitAsync();

            await _bus.Publish(new AppParameterRemovedEvent(request.Id, _user));

            return Unit.Value;
        }

        public async Task<Unit> Handle(AppParameterCreateBundleCommand request, CancellationToken cancellationToken)
        {
            //await SendEmailExample();

            ThrowIfMessageNotValid(request);

            try
            {
                AppParameter appParameter1 = new AppParameter("App Parameter 1", "1", "App Parameter Description 1", _user.Name);
                AppParameter appParameter2 = new AppParameter("App Parameter 2", "2", "App Parameter Description 2", _user.Name);
                AppParameter appParameter3 = new AppParameter("App Parameter 3", "3", "App Parameter Description 3", _user.Name);
                AppParameter appParameter4 = new AppParameter("App Parameter 4", "4", "App Parameter Description 4", _user.Name);
                AppParameter appParameter5 = new AppParameter("App Parameter 5", "5", "App Parameter Description 5", _user.Name);

                await StartTransactionAsync();

                await _appParameterRepository.AddAsync(appParameter1);
                await CommitAsync();

                await _appParameterRepository.AddAsync(appParameter2);
                await CommitAsync();

                await _appParameterRepository.AddAsync(appParameter3);
                await CommitAsync();

                await _appParameterRepository.AddAsync(appParameter4);
                await CommitAsync();

                await _appParameterRepository.AddAsync(appParameter5);
                await CommitAsync();

                if (request.ThrowException)
                    throw new Exception("this exception is throw for demonstration");

                await CommitTransactionAsync();

                await _bus.Publish(new AppParameterCreatedEvent(appParameter1, _user));
                await _bus.Publish(new AppParameterCreatedEvent(appParameter2, _user));
                await _bus.Publish(new AppParameterCreatedEvent(appParameter3, _user));
                await _bus.Publish(new AppParameterCreatedEvent(appParameter4, _user));
                await _bus.Publish(new AppParameterCreatedEvent(appParameter5, _user));

                return Unit.Value;
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        public void Dispose()
        {
            _appParameterRepository.Dispose();
        }
    }
}