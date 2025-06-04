using CodeFirstAproach.contracts.requests;

namespace CodeFirstAproach.Services.abstractions;

public interface IAuthService
{
    public Task<bool> RegisterAsync(string username, string password, CancellationToken cancellationToken);
    public Task<TokenPairDto?> LoginAsync(string username, string password, CancellationToken ct);
}