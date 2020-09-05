using AutoMapper;
using LearnBySpeaking.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LearnBySpeaking.Services.WebApi.Utility
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(AutoMapperConfig));

            // Registering Mappings automatically only works if the 
            // Automapper Profile classes are in ASP.NET project
            AutoMapperConfig.RegisterMappings();
        }
    }
}