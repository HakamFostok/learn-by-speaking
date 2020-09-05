using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.Services;
using LearnBySpeaking.Domain.CommandHandler.Common;
using LearnBySpeaking.Domain.Commands.AppParameter;
using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.EventHandlers;
using LearnBySpeaking.Domain.Events;
using LearnBySpeaking.Domain.Interfaces.Core;
using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Infra.Data.Context;
using LearnBySpeaking.Infra.Data.Repository;
using LearnBySpeaking.Infra.Data.Repository.Core;
using LearnBySpeaking.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LearnBySpeaking.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, User>();

            services.AddSingleton<ISendEmailService, SendEmailService>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediator, MediatR.Mediator>();

            #region AppService Implementation

            services.AddScoped<IAppParameterAppService, AppParameterAppService>();

            #endregion

            #region Domain - Events Implementation

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<AppParameterCreatedEvent>, AppParameterEventHandler>();
            services.AddScoped<INotificationHandler<AppParameterUpdatedEvent>, AppParameterEventHandler>();
            services.AddScoped<INotificationHandler<AppParameterRemovedEvent>, AppParameterEventHandler>();

            #endregion

            #region Domain - Commands Implementation

            services.AddScoped<IRequestHandler<AppParameterCreateCommand, Unit>, AppParameterCommandHandler>();
            services.AddScoped<IRequestHandler<AppParameterUpdateCommand, Unit>, AppParameterCommandHandler>();
            services.AddScoped<IRequestHandler<AppParameterRemoveCommand, Unit>, AppParameterCommandHandler>();
            services.AddScoped<IRequestHandler<AppParameterCreateBundleCommand, Unit>, AppParameterCommandHandler>();

            #endregion

            #region Infra - Data Implementation

            services.AddScoped<IAppParameterRepository, AppParameterRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<BTContext>((provider, optionsBuilder) =>
            {
                IConnectionStrings connectionString = provider.GetService<IConnectionStrings>();
                // this statement can be used for debugging but in production it could cause problem
                //optionsBuilder.EnableSensitiveDataLogging();

                //optionsBuilder.UseSqlServer(connectionString.DefaultConnection);
                // the following feature is nice, but it will not work with Transaction
                //optionsBuilder.UseSqlServer(connectionString.DefaultConnection, builder =>
                //{
                //    // https://stackoverflow.com/q/29840282
                //    builder.EnableRetryOnFailure(2, TimeSpan.FromSeconds(3), null);
                //});
            }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);


            services.AddScoped<IMongoEventRepository, MongoEventRepository>();
            services.AddScoped<EventStoreContext>();

            #endregion
        }
    }
}