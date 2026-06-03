using CatWatch.Domain.Repositories;
using CatWatch.Infrastructure.Auth;
using CatWatch.Infrastructure.Messaging;
using CatWatch.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CatWatch.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));

        services.AddSingleton<IMongoClient>(_ =>
            new MongoClient(configuration.GetConnectionString("MongoDb")));


        services.AddScoped<IProbeRepository, ProbeRepository>();
        services.AddScoped<IReadingRepository, ReadingRepository>();
        services.AddScoped<IShelterRepository, ShelterRepository>();

        AddAuthentication(services);
        AddMessaging(services, configuration);

        return services;
    }


    private static void AddMessaging(IServiceCollection services, IConfiguration configuration)
    {
        var rabbitHost = configuration["RabbitMQ:HostName"];
        if (string.IsNullOrWhiteSpace(rabbitHost))
            return;

        services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
        services.AddSingleton<ILoggerProvider, RabbitMqLoggerProvider>();
    }

    private static void AddAuthentication(IServiceCollection services)
    {
        services.AddAuthentication("ApiKey")
            .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", null);
        services.AddAuthorization();
    }
}
