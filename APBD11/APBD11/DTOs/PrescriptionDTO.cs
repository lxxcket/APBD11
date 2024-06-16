namespace APBD11.DTOs;

public class PrescriptionDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDTO Doctor { get; set; }
    public List<PrescriptionMedicamentDto> PrescriptionMedicaments { get; set; }
}