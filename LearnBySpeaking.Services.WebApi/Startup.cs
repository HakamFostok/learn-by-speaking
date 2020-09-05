using LearnBySpeaking.Application.Services;
using LearnBySpeaking.Domain.Interfaces.Core;
using LearnBySpeaking.Infra.CrossCutting.IoC;
using LearnBySpeaking.Infra.Data.Context;
using LearnBySpeaking.Services.WebApi.HostedServices;
using LearnBySpeaking.Services.WebApi.Utility;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using System;
using System.Text;

namespace LearnBySpeaking.Services.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
              .AddEntityFrameworkStores<LearnBySpeakingContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddNLog());
            ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
            services.AddSingleton(logger);

            services.AddAutoMapperSetup();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            NativeInjectorBootStrapper.RegisterServices(services);

            ConfigureAppSettings(services);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });

            services.AddHostedService<WiredIntegrationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.ConfigureExceptionHandler();
            app.ConfigureLanguageMiddleware();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void ConfigureAppSettings(IServiceCollection services)
        {
            void ConfigureSection<Interface, Implementation>(string sectionName)
                where Implementation : Interface, new() where Interface : class
            {
                Implementation configSection = new Implementation();
                Configuration.GetSection(sectionName).Bind(configSection);
                services.AddSingleton<Interface>(configSection);
            }

            ConfigureSection<IJwtOptions, JwtOptions>(nameof(JwtOptions));
            ConfigureSection<IConnectionStrings, ConnectionStrings>(nameof(ConnectionStrings));
            ConfigureSection<IMongodbDatabaseSettings, MongodbDatabaseSettings>(nameof(MongodbDatabaseSettings));

            services.AddSingleton<IAppSettings>(factory =>
            {
                IJwtOptions jwtOptionsSection = factory.GetService<IJwtOptions>();
                IConnectionStrings connectionStringsSection = factory.GetService<IConnectionStrings>();
                IMongodbDatabaseSettings mongodbDatabaseSettings = factory.GetService<IMongodbDatabaseSettings>();

                return new AppSettings(jwtOptionsSection, connectionStringsSection, mongodbDatabaseSettings);
            });
        }
        
    }
}