using CodeFirstAproach.Model;

namespace CodeFirstAproach.Repositories.abstractions;

public interface IPatientRepository
{
    public Task<bool> createPatientIfDoesntExist(Patient patient, CancellationToken token);
}