using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Interfaces
{
  
    public interface IBaseAppService<TViewModel, TEventModel> : IDisposable
    where TViewModel : BaseViewModel
    where TEventModel : Event
    {
        #region Query Operations

        Task<IQueryable<TViewModel>> GetAllAsync();
        Task<TViewModel> GetByIdAsync(int id);

        #endregion

        #region CRUD Operations

        Task CreateAsync(TViewModel viewModel);
        Task UpdateAsync(TViewModel viewModel);
        Task RemoveAsync(int id);

        #endregion

        #region Event Operations

        Task<IQueryable<TEventModel>> GetAllEventAsync();
        Task<IQueryable<TEventModel>> GetAllEventByRefKeyAsync(int id);
        Task<IQueryable<TEventModel>> GetAllEventByObjectIdAsync(string mongoId);

        #endregion
    }
}