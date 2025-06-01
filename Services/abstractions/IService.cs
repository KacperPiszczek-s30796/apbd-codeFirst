using CodeFirstAproach.contracts.requests;

namespace CodeFirstAproach.Services.abstractions;

public interface IService
{
    public Task<bool> issuePrescription(requestDTO request, CancellationToken cancellationToken);
}