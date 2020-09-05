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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}