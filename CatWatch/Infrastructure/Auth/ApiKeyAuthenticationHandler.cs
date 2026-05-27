using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace CatWatch.Infrastructure.Auth;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly string _configuredKey;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IConfiguration config) : base(options, logger, encoder)
    {
        _configuredKey = config["Auth:ApiKey"] ?? string.Empty;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (string.IsNullOrEmpty(_configuredKey))
        {
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity("ApiKey")), Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        var apiKey = Request.Headers["X-Api-Key"].FirstOrDefault();
        if (apiKey != _configuredKey)
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key"));

        var successTicket = new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity("ApiKey")), Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(successTicket));
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 401;
        return Task.CompletedTask;
    }

    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 403;
        return Task.CompletedTask;
    }
}