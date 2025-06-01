namespace CodeFirstAproach.contracts.response;

public class PrescriptionResponseDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentResponseDTO> Medicaments { get; set; }
    public DoctorResponseDTO Doctor { get; set; }
}