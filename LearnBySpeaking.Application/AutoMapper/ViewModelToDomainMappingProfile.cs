using AutoMapper;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Commands.AppParameter;

namespace LearnBySpeaking.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region AppParameter Mapping Configuration

            CreateMap<AppParameterViewModel, AppParameterCreateCommand>()
                .ConstructUsing(c => new AppParameterCreateCommand(c.Name, c.Value, c.Description));

            CreateMap<AppParameterViewModel, AppParameterUpdateCommand>()
                .ConstructUsing(c => new AppParameterUpdateCommand(c.Id, c.Name, c.Value, c.Description));

            #endregion
        }
    }
}