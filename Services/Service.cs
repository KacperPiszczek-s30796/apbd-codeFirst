using CodeFirstAproach.contracts.requests;
using CodeFirstAproach.Model;
using CodeFirstAproach.Repositories.abstractions;
using CodeFirstAproach.Services.abstractions;

namespace CodeFirstAproach.Services;

public class Service: IService
{
    private IMedicamentRepository medicamentRepository { get; set; }
    private IPatientRepository patientRepository { get; set; }
    private IPrescriptionRepository prescriptionRepository { get; set; }
    private IDoctorRepository doctorRepository { get; set; }

    public Service(IMedicamentRepository medicamentRepository, IPatientRepository patientRepository,
        IPrescriptionRepository prescriptionRepository, IDoctorRepository doctorRepository)
    {
        this.medicamentRepository = medicamentRepository;
        this.patientRepository = patientRepository;
        this.prescriptionRepository = prescriptionRepository;
        this.doctorRepository = doctorRepository;
    }

    public async Task<bool> issuePrescription(requestDTO request, CancellationToken cancellationToken)
    {
        Patient patient = new Patient()
        {
            IdPatient = request.patient.IdPatient,
            FirstName = request.patient.FirstName,
            LastName = request.patient.LastName,
            BirthDate = request.patient.BrirthDate
        };
        List<Medicament> medicaments = new List<Medicament>();
        foreach (var medicament in request.medicaments)
        {
            Medicament m = new Medicament()
            {

            };
            medicaments.Add(m);
        }
        return true;
    }
}