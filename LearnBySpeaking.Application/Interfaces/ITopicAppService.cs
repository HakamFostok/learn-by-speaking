using LearnBySpeaking.Application.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Interfaces
{
    public interface ITopicAppService
    {
        Task<IQueryable<TopicViewModel>> GetAllAsync();
    }
}