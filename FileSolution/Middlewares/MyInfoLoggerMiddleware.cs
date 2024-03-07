using System.Runtime.CompilerServices;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Middlewares
{
    public class MyInfoLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyInfoLoggerMiddleware> _logger;
        private readonly string _methodname;

        public MyInfoLoggerMiddleware(RequestDelegate next, ILogger<MyInfoLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _methodname = GetType().Name;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"{_methodname} ran. Time: {DateTime.Now.ToString()}");
            await _next(context);
        }
    }

    public static class MyInfoLoggerMiddlewareExtension
    {
        public static IApplicationBuilder UseMyInfoLogger(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyInfoLoggerMiddleware>();
        }
    }
}
