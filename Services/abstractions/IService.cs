using CodeFirstAproach.contracts.requests;
using CodeFirstAproach.contracts.response;

namespace CodeFirstAproach.Services.abstractions;

public interface IService
{
    public Task<bool> issuePrescription(requestDTO request, CancellationToken cancellationToken);
    public Task<responseDTO> GetPatientInfo(int patientID, CancellationToken cancellationToken);
}