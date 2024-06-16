namespace APBD11.DTOs;

public class PatientWithPrescriptionsDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionDTO> Prescriptions { get; set; }
}