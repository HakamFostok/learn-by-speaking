using AutoMapper;
using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Commands.AppParameter;
using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.Events;
using LearnBySpeaking.Domain.Interfaces.Core;
using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Services
{
    public class AppParameterAppService : BaseAppService<AppParameterViewModel, AppParameter, AppParameterEventModel, AppParameterCreateCommand, AppParameterUpdateCommand, AppParameterRemoveCommand>,
        IAppParameterAppService
    {
        private readonly IAppParameterRepository _appParameterRepository;
        private readonly ISendEmailService _sendEmailService;

        public AppParameterAppService(IMapper mapper,
                                      IMediator bus,
                                      IAppParameterRepository appParameterRepository,
                                      IMongoEventRepository mongoEventRepository,
                                      IUser user,
                                      ISendEmailService sendEmailService)
            : base(mapper, appParameterRepository, mongoEventRepository, bus, user)
        {
            _appParameterRepository = appParameterRepository;
            _sendEmailService = sendEmailService;
        }

        public async Task CreateBundleAsync(bool throwException)
        {
            AppParameterCreateBundleCommand createBundleCommand = new AppParameterCreateBundleCommand(throwException);
            await _bus.Send(createBundleCommand);
        }

        public async Task<AppParameterViewModel> GetByNameAsync(string name)
        {
            AppParameter result = await _appParameterRepository.GetAppParameterByNameAsync(name);
            return _mapper.Map<AppParameterViewModel>(result);
        }

        public async Task SendEmail()
        {
            await _sendEmailService.SendEmailAsync("Test email service",
                "Hi. <br> This is Hakan and he is testing a new email service, if you received this email please told him, he will be very happy that the service is working.<br> Thank you. <br> Have a nice day",
                "hakam.fostok@biteg.net",
                new List<string> { "alitaha.ozturk@biteg.net", "omer.ozkok@biteg.net" });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}