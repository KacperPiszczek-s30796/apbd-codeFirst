using CodeFirstAproach.contracts.requests;
using CodeFirstAproach.contracts.response;
using CodeFirstAproach.exceptions;
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
                throw new medicament10limitBreachedException();
            }
            PrescriptionMedicament m = new PrescriptionMedicament()
            {
                IdMedicament = medicament.IdMedicament,
                Dose = medicament.Dose,
                Details = medicament.Description
            };
            if (!(await medicamentRepository.DoesMedicamentExist(medicament.IdMedicament, cancellationToken)))
            {
                throw new MedicationDoesnotExistException(medicament.IdMedicament);
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
        List<Prescription> prescriptions = await prescriptionRepository.GetPrescriptionsByPatientId(patientID, cancellationToken);
        responseDTO.IdPatient = patientID;
        Patient patient = prescriptions[0].Patient;
        responseDTO.FirstName = patient.FirstName;
        responseDTO.LastName = patient.LastName;
        responseDTO.Birthdate = patient.BirthDate;
        List<PrescriptionResponseDTO> prescriptionResponses = new List<PrescriptionResponseDTO>();
        foreach (var prescription in prescriptions)
        {
            PrescriptionResponseDTO prescriptionResponseDTO = new PrescriptionResponseDTO();
            prescriptionResponseDTO.IdPrescription = prescription.IdPrescription;
            prescriptionResponseDTO.Date = prescription.Date;
            prescriptionResponseDTO.DueDate = prescription.DueDate;
            DoctorResponseDTO doctorResponseDTO = new DoctorResponseDTO();
            doctorResponseDTO.IdDoctor = prescription.Doctor.IdDoctor;
            doctorResponseDTO.FirstName = prescription.Doctor.FirstName;
            doctorResponseDTO.LastName = prescription.Doctor.LastName;
            doctorResponseDTO.Email = prescription.Doctor.Email;
            prescriptionResponseDTO.Doctor = doctorResponseDTO;
            List<MedicamentResponseDTO> medicamentResponses = new List<MedicamentResponseDTO>();
            List<PrescriptionMedicament> medicaments = await prescriptionRepository.GetPrescriptionMedicamentsByPrescriptionIdAsync(prescription.IdPrescription, cancellationToken);
            foreach (var medicament in medicaments)
            {
                MedicamentResponseDTO medicamentResponseDTO = new MedicamentResponseDTO();
                medicamentResponseDTO.IdMedicament = medicament.IdMedicament;
                medicamentResponseDTO.Dose = medicament.Dose;
                medicamentResponseDTO.Details = medicament.Details;
                medicamentResponseDTO.Name = medicament.Medicament.Name;
                medicamentResponseDTO.Description = medicament.Medicament.Description;
                medicamentResponseDTO.Type = medicament.Medicament.Type;
                medicamentResponses.Add(medicamentResponseDTO);
            }
            prescriptionResponseDTO.Medicaments = medicamentResponses;
            prescriptionResponses.Add(prescriptionResponseDTO);
        }
        responseDTO.Prescriptions = prescriptionResponses;
        return responseDTO;
    }
}