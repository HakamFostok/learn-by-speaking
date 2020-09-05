using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Services
{
    public class TestAppService : ITestAppService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestAppService(IMapper mapper,
                              ITestRepository testRepository)
        {
            _mapper = mapper;
            _testRepository = testRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _testRepository.RemoveAsync(id);
        }

        public async Task<IQueryable<TestViewModel>> GetAllAsync()
        {
            var result = await _testRepository.GetAllAsync();
            return result.ProjectTo<TestViewModel>(_mapper.ConfigurationProvider);
        }
    }
}