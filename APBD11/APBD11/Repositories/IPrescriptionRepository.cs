using APBD11.Entities;

namespace APBD11.Repositories;

public interface IPrescriptionRepository
{
    Task<int> AddPrescription(Prescription prescription);
}