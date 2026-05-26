using CatWatch.Domain.Repositories;
using CatWatch.Infrastructure.Messaging;
using CatWatch.Infrastructure.Persistence;
using MongoDB.Driver;

namespace CatWatch.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(_ =>
            new MongoClient(configuration.GetConnectionString("MongoDb")));


        services.AddScoped<IProbeRepository, ProbeRepository>();
        services.AddScoped<IReadingRepository, ReadingRepository>();
        services.AddScoped<IShelterRepository, ShelterRepository>();

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
}
