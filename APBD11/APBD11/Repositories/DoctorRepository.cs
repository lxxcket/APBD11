using APBD11.Contexts;
using APBD11.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private MedicamentContext _medicamentContext;

    public DoctorRepository(MedicamentContext medicamentContext)
    {
        _medicamentContext = medicamentContext;
    }

    public async Task<Doctor?> GetAnyDoctor()
    {
        return await _medicamentContext.Doctors.FirstOrDefaultAsync();
    }
}