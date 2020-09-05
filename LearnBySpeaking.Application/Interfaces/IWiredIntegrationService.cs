using System;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Interfaces
{
    public interface IWiredIntegrationAppService : IDisposable
    {
        Task Integration();
    }
}