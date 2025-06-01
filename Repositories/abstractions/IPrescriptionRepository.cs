using CodeFirstAproach.Model;

namespace CodeFirstAproach.Repositories.abstractions;

public interface IPrescriptionRepository
{
    public Task<bool> AddPrescription(Prescription prescription,
        List<PrescriptionMedicament> prescriptionMedicaments, CancellationToken token);
}