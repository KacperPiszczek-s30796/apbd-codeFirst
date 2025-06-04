using CodeFirstAproach.contracts.requests;

namespace CodeFirstAproach.Services.abstractions;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
}