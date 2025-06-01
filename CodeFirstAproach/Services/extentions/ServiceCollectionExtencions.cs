using CodeFirstAproach.Services.abstractions;

namespace CodeFirstAproach.Services.extentions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IService, Service>();

        return services;
    }
}