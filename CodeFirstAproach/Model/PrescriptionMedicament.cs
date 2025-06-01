using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstAproach.Model;

public class PrescriptionMedicament
{
    [Required]
    public int IdMedicament { get; set; }
    [ForeignKey("IdMedicament")]
    public virtual Medicament Medicament { get; set; }
    [Required]
    public int IdPrescription { get; set; }
    [ForeignKey("IdPrescription")]
    public virtual Prescription Prescription { get; set; }
    public int Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
}