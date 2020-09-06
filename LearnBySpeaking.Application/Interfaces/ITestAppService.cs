using LearnBySpeaking.Application.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Interfaces
{
    public interface ITestAppService
    {
        Task<IQueryable<TestViewModel>> GetAllAsync();
        Task DeleteAsync(int id);
        Task CreateTest(CreateTestViewModel model);
    }
}