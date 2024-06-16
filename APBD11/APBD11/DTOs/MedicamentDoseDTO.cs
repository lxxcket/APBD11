namespace APBD11.DTOs;

public class MedicamentDoseDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}