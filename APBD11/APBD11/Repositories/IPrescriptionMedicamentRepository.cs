using APBD11.DTOs;

namespace APBD11.Repositories;

public interface IPrescriptionMedicamentRepository
{
    Task<int> AddPrescriptionMedicament(int idPrescription, MedicamentDoseDTO medicamentDoseDto);
}