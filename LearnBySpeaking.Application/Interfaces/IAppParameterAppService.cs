using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Events;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Interfaces
{
    public interface IAppParameterAppService : IBaseAppService<AppParameterViewModel, AppParameterEventModel>
    {
        Task CreateBundleAsync(bool throwException);

        Task<AppParameterViewModel> GetByNameAsync(string name);
    }
}