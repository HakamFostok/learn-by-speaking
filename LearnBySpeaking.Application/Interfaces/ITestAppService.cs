using LearnBySpeaking.Application.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Interfaces
{
    public interface ITestAppService
    {
        Task<IQueryable<TestViewModel>> GetAllAsync();
        Task<TestViewModel> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task CreateTest(CreateTestViewModel model);
        Task<EvaluateTest> TakeTest(EvaluateTest model);
    }
}