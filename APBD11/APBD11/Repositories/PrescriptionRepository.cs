using APBD11.Contexts;
using APBD11.Entities;

namespace APBD11.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private MedicamentContext _medicamentContext;

    public PrescriptionRepository(MedicamentContext medicamentContext)
    {
        _medicamentContext = medicamentContext;
    }

    public async Task<int> AddPrescription(Prescription prescription)
    {
        await _medicamentContext.Prescriptions.AddAsync(prescription);
        await _medicamentContext.SaveChangesAsync();
        return prescription.IdPrescription;
    }
}