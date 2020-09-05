﻿using AutoMapper;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Models;

namespace LearnBySpeaking.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AppParameter, AppParameterViewModel>();
        }
    }
}