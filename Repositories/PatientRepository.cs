using CodeFirstAproach.DAL;
using CodeFirstAproach.Model;
using CodeFirstAproach.Repositories.abstractions;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstAproach.Repositories;

public class PatientRepository: IPatientRepository
{
    private DbContext1 _context1;

    public PatientRepository(DbContext1 context1)
    {
        _context1 = context1;
    }

    public async Task<bool> createPatientIfDoesntExist(Patient patient, CancellationToken token)
    {
            var existingPatient = await _context1.Patients
                .FirstOrDefaultAsync(p => p.IdPatient == patient.IdPatient, token);

            if (existingPatient != null)
            {
                return false;
            }

            _context1.Patients.Add(patient);
            await _context1.SaveChangesAsync(token);

            return true;
        }
}