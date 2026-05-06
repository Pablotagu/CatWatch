using Microsoft.AspNetCore.Authorization;

namespace CatWatch.Infrastructure.Auth;

public class ApiKeyHandler : AuthorizationHandler<ApiKeyRequirement>
{
    private readonly string _configuredKey;

    public ApiKeyHandler(IConfiguration config)
    {
        _configuredKey = config["Auth:ApiKey"] ?? string.Empty;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
    {
        var httpContext = context.Resource as HttpContext;

        if(httpContext == null)
            return Task.CompletedTask;
        
        if(string.IsNullOrEmpty(_configuredKey))
            return Task.CompletedTask;

        var apiKey = httpContext?.Request.Headers["X-Api-Key"].FirstOrDefault() ?? string.Empty;

        if (apiKey == _configuredKey)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
