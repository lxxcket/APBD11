using APBD11.Entities;
using APBD11.DTOs;

namespace APBD11.UseCases;

public interface IGetPatientService
{
    Task<PatientWithPrescriptionsDTO> GetPatient(int idPatient);
}