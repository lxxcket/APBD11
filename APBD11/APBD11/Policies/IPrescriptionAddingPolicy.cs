using APBD11.Repositories;
using APBD11.DTOs;
using APBD11.Entities;

namespace APBD11.Policies;

public interface IPrescriptionAddingPolicy
{
    Task<bool> PatientExists(Patient patient);
    Task<bool> MedicamentExists(Medicament medicament);
    bool IsMedicamentListOver10Elements(List<MedicamentDoseDTO> medicaments);
    bool IsDueDateGreaterThanDate(DateTime dueDate, DateTime date);
}