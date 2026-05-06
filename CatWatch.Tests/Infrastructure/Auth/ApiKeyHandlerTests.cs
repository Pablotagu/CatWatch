using System.Security.Claims;
using CatWatch.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace CatWatch.Tests.Infrastructure.Auth;

public class ApiKeyHandlerTests
{
     private const string ValidApiKey = "test-api-key";

    private ApiKeyHandler CreateHandler(string? configuredKey = ValidApiKey)
    {
        var config = Substitute.For<IConfiguration>();
        config["Auth:ApiKey"].Returns(configuredKey);
        return new ApiKeyHandler(config);
    }

    private AuthorizationHandlerContext CreateContext(string? headerApiKey = null)
    {
        var httpContext = new DefaultHttpContext();
        if (headerApiKey is not null)
            httpContext.Request.Headers["X-Api-Key"] = headerApiKey;

        return new AuthorizationHandlerContext(
            [new ApiKeyRequirement()],
            new ClaimsPrincipal(),
            httpContext);
    }
    
    [Fact]
    public async Task HandleAsync_WhenApiKeyIsValid_Succeeds()
    {
        var handler = CreateHandler();
        var context = CreateContext(ValidApiKey);

        await handler.HandleAsync(context);
        Assert.True(context.HasSucceeded);
    }

    [Fact]
    public async Task HandleAsync_WhenApiKeyIsInvalid_DoesNotSucceed()
    {
        var handler = CreateHandler();
        var context = CreateContext("wrong-key");

        await handler.HandleAsync(context);
        Assert.False(context.HasSucceeded);
    }

    [Fact]
    public async Task HandleAsync_WhenNoApiKeyHeader_DoesNotSucceed()
    {
        var handler = CreateHandler();
        var context = CreateContext();

        await handler.HandleAsync(context);
        Assert.False(context.HasSucceeded);
    }

    [Fact]
    public async Task HandleAsync_WhenNoConfiguredKey_DoesNotSucceed()
    {
        var handler = CreateHandler(null);
        var context = CreateContext(ValidApiKey);

        await handler.HandleAsync(context);
        Assert.False(context.HasSucceeded);
    }
}
