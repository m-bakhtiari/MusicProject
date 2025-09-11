using System;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;

public class ExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILog _logger;

    public ExceptionLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        _logger = LogManager.GetLogger(typeof(ExceptionLoggingMiddleware));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Error("Unhandled exception occurred", ex);
            throw; // اجازه بده ASP.NET Core خودش هندل کنه
        }
    }
}