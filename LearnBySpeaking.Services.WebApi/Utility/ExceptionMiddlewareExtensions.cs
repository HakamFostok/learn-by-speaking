using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.Utility
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Program> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<Program> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            IExceptionHandlerFeature exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (exceptionHandlerFeature == null)
                return;

            context.Response.Clear();
            context.Response.ContentType = "application/json";

            string jsonResult = GetJsonOfException(context, exceptionHandlerFeature);

            await context.Response.WriteAsync(jsonResult);
        }

        private string GetJsonOfException(HttpContext context, IExceptionHandlerFeature exceptionHandlerFeature)
        {
            Exception ex = exceptionHandlerFeature.Error;
            string path = context.Request.Path;

            if (ex is ValidationException valException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return JsonConvert.SerializeObject(new
                {
                    ErrorMessages = valException.Errors.Select(x => x.ErrorMessage).ToList()
                });
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ex.Data.Add("path", path);

                string exceptionId = Guid.NewGuid().ToString();

                EventId eventId = new EventId(0, exceptionId);
                _logger.LogError(eventId: eventId,
                    exception: ex,
                    message: $"Date : {DateTime.Now.ToString()} \nPath : {path} \nStackTrace: {ex.ToString()}");

                return JsonConvert.SerializeObject(new
                {
                    Guid = exceptionId,
                    Path = path,
                    ErrorMessage = ex.Message,
                    ex.StackTrace,
                });
            }
        }
    }

    //https://code-maze.com/global-error-handling-aspnetcore/
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError => appError.UseMiddleware<ExceptionMiddleware>());
        }
    }
}