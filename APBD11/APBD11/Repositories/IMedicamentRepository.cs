using APBD11.Contexts;
using APBD11.Entities;

namespace APBD11.Repositories;

public interface IMedicamentRepository
{
    Task<Medicament?> GetMedicament(Medicament medicament);
    
}