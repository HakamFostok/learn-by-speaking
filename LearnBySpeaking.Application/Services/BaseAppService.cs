using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.Interfaces.Core;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Services
{
    public class BaseAppService<TViewModel, TModel, TEventModel, TCommandCreate, TCommandUpdate, TCommandDelete> : IBaseAppService<TViewModel, TEventModel>
        where TViewModel : BaseViewModel, new()
        where TEventModel : Event
        where TModel : class
        where TCommandCreate : Command
        where TCommandUpdate : Command
        where TCommandDelete : Command, new()
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository<TModel> _repo;
        protected readonly IMongoEventRepository _mongoEventRepository;
        protected readonly IMediator _bus;
        protected readonly IUser _user;

        public BaseAppService(IMapper mapper,
            IRepository<TModel> repo,
            IMongoEventRepository mongoEventRepository,
            IMediator bus,
            IUser user)
        {
            _mapper = mapper;
            _repo = repo;
            _mongoEventRepository = mongoEventRepository;
            _bus = bus;
            _user = user;
        }

        #region Query Operation

        public async Task<IQueryable<TViewModel>> GetAllAsync()
        {
            IQueryable<TModel> result = await _repo.GetAllAsync();
            return result.ProjectTo<TViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<TViewModel> GetByIdAsync(int id)
        {
            TModel result = await _repo.GetByIdAsync(id);
            return _mapper.Map<TViewModel>(result);
        }

        #endregion

        #region GRUD Operations

        public async Task CreateAsync(TViewModel viewModel)
        {
            TCommandCreate createCommand = _mapper.Map<TCommandCreate>(viewModel);
            await _bus.Send(createCommand);
        }

        public async Task UpdateAsync(TViewModel viewModel)
        {
            TCommandUpdate updateCommand = _mapper.Map<TCommandUpdate>(viewModel);
            await _bus.Send(updateCommand);
        }

        public async Task RemoveAsync(int id)
        {
            TCommandDelete removeCommand = new TCommandDelete() { Id = id };
            await _bus.Send(removeCommand);
        }

        #endregion

        #region Event Operations

        public async Task<IQueryable<TEventModel>> GetAllEventAsync() =>
            await _mongoEventRepository.GetList<TEventModel>(x => x.TableName == ConvertTableName());

        public async Task<IQueryable<TEventModel>> GetAllEventByRefKeyAsync(int id) =>
            await _mongoEventRepository.GetList<TEventModel>(x => x.TableName == ConvertTableName() && x.AggregateId == id);

        public async Task<IQueryable<TEventModel>> GetAllEventByObjectIdAsync(string mongoId) =>
            await _mongoEventRepository.GetList<TEventModel>(x => x.TableName == ConvertTableName() && x.MongoId == mongoId);

        #endregion

        private string ConvertTableName()
        {
            string pureClassname = new TViewModel().ToString().Split('.').Last();
            return pureClassname.Replace("ViewModel", "");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}