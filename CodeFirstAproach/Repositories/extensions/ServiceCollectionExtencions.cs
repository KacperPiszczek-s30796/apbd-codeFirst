using CodeFirstAproach.Repositories.abstractions;

namespace CodeFirstAproach.Repositories.extensions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IMedicamentRepository, MedicamentRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}