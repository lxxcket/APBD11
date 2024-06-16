using APBD11.DTOs;
using APBD11.Entities;

namespace APBD11.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetPatientByPatientDetails(Patient patient);

    Task<int> CreatePatient(Patient patient);

    Task<PatientWithPrescriptionsDTO?> GetPatientById(int id);
}