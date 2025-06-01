namespace CodeFirstAproach.Repositories.abstractions;

public interface IMedicamentRepository
{
    public Task<bool> DoesMedicamentExist(int id, CancellationToken token);
}