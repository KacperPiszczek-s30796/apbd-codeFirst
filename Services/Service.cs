using CodeFirstAproach.contracts.requests;
using CodeFirstAproach.contracts.response;
using CodeFirstAproach.Model;
using CodeFirstAproach.Repositories;
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
        if (request.DueDate < request.Date) return false;
        Patient patient = new Patient()
        {
            IdPatient = request.patient.IdPatient,
            FirstName = request.patient.FirstName,
            LastName = request.patient.LastName,
            BirthDate = request.patient.BrirthDate
        };
        List<PrescriptionMedicament> medicaments = new List<PrescriptionMedicament>();
        int i = 0;
        foreach (var medicament in request.medicaments)
        {
            if (++i > 10)
            {
                return false;
            }
            PrescriptionMedicament m = new PrescriptionMedicament()
            {
                IdMedicament = medicament.IdMedicament,
                Dose = medicament.Dose,
                Details = medicament.Description
            };
            if (!(await medicamentRepository.DoesMedicamentExist(medicament.IdMedicament, cancellationToken)))
            {
                return false;
            }
            
            medicaments.Add(m);
        }
        await patientRepository.createPatientIfDoesntExist(patient, cancellationToken);
        Prescription prescription = new Prescription()
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = request.doctor.IdDoctor
        };
        await prescriptionRepository.AddPrescription(prescription, medicaments, cancellationToken);
        return true;
    }

    public async Task<responseDTO> GetPatientInfo(int patientID, CancellationToken cancellationToken)
    {
        responseDTO responseDTO = new responseDTO();
        
        return responseDTO;
    }
}