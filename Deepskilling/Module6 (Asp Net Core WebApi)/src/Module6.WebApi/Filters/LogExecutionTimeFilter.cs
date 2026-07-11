using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Module6.WebApi.Filters;

public class LogExecutionTimeFilter : IActionFilter
{
    private readonly ILogger<LogExecutionTimeFilter> _logger;
    private Stopwatch _stopwatch = new();

    public LogExecutionTimeFilter(ILogger<LogExecutionTimeFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
        _logger.LogInformation("Executing {Action}", context.ActionDescriptor.DisplayName);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        _logger.LogInformation(
            "Executed {Action} in {ElapsedMilliseconds} ms with status {StatusCode}",
            context.ActionDescriptor.DisplayName,
            _stopwatch.ElapsedMilliseconds,
            context.HttpContext.Response.StatusCode);
    }
}
