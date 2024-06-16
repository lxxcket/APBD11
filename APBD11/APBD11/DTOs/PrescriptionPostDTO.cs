using APBD11.Entities;

namespace APBD11.DTOs;

public class PrescriptionPostDTO
{
    public PatientDTO Patient { get; set; }
    public List<MedicamentDoseDTO> Medicaments { get; set; } = new List<MedicamentDoseDTO>();
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}