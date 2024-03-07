using System.Runtime.CompilerServices;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MyAPI
{
    public class MyInfoLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyInfoLoggerMiddleware> _logger;
        private readonly string _methodName;

        public MyInfoLoggerMiddleware(RequestDelegate next, ILogger<MyInfoLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _methodName = GetType().Name;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"{_methodName} before next. Time: {DateTime.Now.ToString()}");
            await _next(context);
            _logger.LogInformation($"{_methodName} after next. Time: {DateTime.Now.ToString()}");
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
