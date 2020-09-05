using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Services
{
    public class TopicAppService : ITopicAppService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicAppService(IMapper mapper,
                               ITopicRepository topicRepository)
        {
            _mapper = mapper;
            _topicRepository = topicRepository;
        }

        public async Task<IQueryable<TopicViewModel>> GetAllAsync()
        {
            var result = await _topicRepository.GetAllAsync();
            return result.ProjectTo<TopicViewModel>(_mapper.ConfigurationProvider);
        }
    }
}