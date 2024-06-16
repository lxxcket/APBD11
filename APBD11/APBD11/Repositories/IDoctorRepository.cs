using APBD11.Entities;

namespace APBD11.Repositories;

public interface IDoctorRepository
{
    Task<Doctor?> GetAnyDoctor();
}