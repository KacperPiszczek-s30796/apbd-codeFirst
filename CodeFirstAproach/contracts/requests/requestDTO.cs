using CodeFirstAproach.Model;

namespace CodeFirstAproach.contracts.requests;

public class requestDTO
{
    public PatientRequestDTO patient { get; set; }
    public List<MedicamentsRequestDTO> medicaments { get; set; }
    public DoctorRequestDTO doctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}