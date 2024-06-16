using APBD11.Contexts;
using APBD11.DTOs;
using APBD11.Entities;

namespace APBD11.Repositories;

public class PrescriptionMedicamentRepository : IPrescriptionMedicamentRepository
{
    private MedicamentContext _medicamentContext;

    public PrescriptionMedicamentRepository(MedicamentContext medicamentContext)
    {
        _medicamentContext = medicamentContext;
    }
    public async Task<int> AddPrescriptionMedicament(int idPrescription, MedicamentDoseDTO medicamentDoseDto)
    {
        var entity = new PrescriptionMedicament()
        {
            IdPrescription = idPrescription,
            IdMedicament = medicamentDoseDto.IdMedicament,
            Details = medicamentDoseDto.Details,
            Dose = medicamentDoseDto.Dose
        };
        await _medicamentContext.PrescriptionMedicaments.AddAsync(entity);
        return await _medicamentContext.SaveChangesAsync();
    }
}