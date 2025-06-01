using CodeFirstAproach.Model;

namespace CodeFirstAproach.Repositories.abstractions;

public interface IPrescriptionRepository
{
    public Task<bool> AddPrescription(Prescription prescription,
        List<PrescriptionMedicament> prescriptionMedicaments, CancellationToken token);

    public Task<List<Prescription>> GetPrescriptionsByPatientId(int patientId, CancellationToken token);

    public Task<List<PrescriptionMedicament>> GetPrescriptionMedicamentsByPrescriptionIdAsync(
        int idPrescription,
        CancellationToken token);
}