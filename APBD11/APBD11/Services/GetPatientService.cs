using APBD11.Entities;
using APBD11.DTOs;
using APBD11.Exceptions;
using APBD11.Repositories;

namespace APBD11.UseCases;

public class GetPatientService : IGetPatientService
{
    private IPatientRepository _patientRepository;

    public GetPatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    public async Task<PatientWithPrescriptionsDTO> GetPatient(int idPatient)
    {
        PatientWithPrescriptionsDTO? patient = await _patientRepository.GetPatientById(idPatient);
        if (patient == null)
        {
            throw new DomainException("Patient does not exist");
        }
        
        return patient;
    }
}