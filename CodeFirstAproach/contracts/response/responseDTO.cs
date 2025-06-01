namespace CodeFirstAproach.contracts.response;

public class responseDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionResponseDTO> Prescriptions { get; set; }
}