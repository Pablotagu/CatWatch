using CatWatch.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }


    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is NotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { error = exception.Message }, cancellationToken);
            return true;
        }

        if (exception is ConflictException)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsJsonAsync(new { error = exception.Message }, cancellationToken);
            return true;
        }

        if (exception is RepositoryException)
        {
            _logger.LogError(exception, "Repository error occurred");
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsJsonAsync(new { error = exception.Message }, cancellationToken);
            return true;
        }

        _logger.LogError(exception, "Unexpected error occurred");
        return false; // deja pasar las excepciones no controladas
    }
}