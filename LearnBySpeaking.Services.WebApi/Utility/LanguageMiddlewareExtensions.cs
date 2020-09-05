using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.Utility
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            StringValues lang = context.Request.Headers["Accept-Language"];

            Thread.CurrentThread.CurrentUICulture = lang == "en" ? new CultureInfo("en-US") : new CultureInfo("tr-TR");

            await _next(context);
        }
    }

    public static class LanguageMiddlewareExtensions
    {
        public static void ConfigureLanguageMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<LanguageMiddleware>();
        }
    }
}