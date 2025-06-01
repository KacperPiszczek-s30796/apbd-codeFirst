using CodeFirstAproach.DAL;
using CodeFirstAproach.Model;
using CodeFirstAproach.Repositories.abstractions;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Prescription>> GetPrescriptionsByPatientId(int patientId, CancellationToken token)
    {
        return await _context1.Prescriptions
            .Where(p => p.IdPatient == patientId)
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .ToListAsync(token);
    }
    public async Task<List<PrescriptionMedicament>> GetPrescriptionMedicamentsByPrescriptionIdAsync(
        int idPrescription,
        CancellationToken token)
    {
        return await _context1.PrescriptionMedicaments
            .Where(pm => pm.IdPrescription == idPrescription)
            .Include(pm => pm.Medicament)
            .ToListAsync(token);
    }
}