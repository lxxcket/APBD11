namespace APBD11.DTOs;

public class PrescriptionMedicamentDto
{
    public int IdMedicament { get; set; }
    public MedicamentDTO Medicament { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}