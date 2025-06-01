using CodeFirstAproach.DAL;
using CodeFirstAproach.Model;
using CodeFirstAproach.Repositories.abstractions;

namespace CodeFirstAproach.Repositories;

public class PrescriptionRepository: IPrescriptionRepository
{
    private DbContext1 _context1;

    public PrescriptionRepository(DbContext1 context1)
    {
        _context1 = context1;
    }

    public async Task<bool> AddPrescription(Prescription prescription,
        List<PrescriptionMedicament> prescriptionMedicaments, CancellationToken token)
    {
        _context1.Prescriptions.Add(prescription);
        await _context1.SaveChangesAsync(token);
        foreach (var prescriptionMedicament in prescriptionMedicaments)
        {
            prescriptionMedicament.IdPrescription = prescription.IdPrescription;
            _context1.PrescriptionMedicaments.Add(prescriptionMedicament);
        }
        await _context1.SaveChangesAsync(token);
        return true;
    }
}