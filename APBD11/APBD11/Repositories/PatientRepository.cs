using APBD11.Contexts;
using APBD11.DTOs;
using APBD11.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Repositories;

public class PatientRepository : IPatientRepository
{
    private MedicamentContext _medicamentContext;

    public PatientRepository(MedicamentContext medicamentContext)
    {
        _medicamentContext = medicamentContext;
    }
    
    public async Task<Patient?> GetPatientByPatientDetails(Patient patient)
    {
        return await _medicamentContext.Patients.FirstOrDefaultAsync(p => p.FirstName == patient.FirstName &&
                                                                          p.LastName == patient.LastName && 
                                                                          p.Birthdate == patient.Birthdate);
    }

    public async Task<int> CreatePatient(Patient patient)
    {
        await _medicamentContext.Patients.AddAsync(patient);
        await _medicamentContext.SaveChangesAsync();
        return patient.IdPatient;
    }

    public async Task<PatientWithPrescriptionsDTO?> GetPatientById(int id)
    {
        return await _medicamentContext.Patients
            .Where(p => p.IdPatient == id)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Select(p => new PatientWithPrescriptionsDTO()
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birthdate = p.Birthdate,
                Prescriptions = p.Prescriptions.Select(pr => new PrescriptionDTO()
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Doctor = new DoctorDTO()
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName,
                        LastName = pr.Doctor.LastName
                    },
                    PrescriptionMedicaments = pr.PrescriptionMedicaments.Select(pm => new PrescriptionMedicamentDto
                    {
                        IdMedicament = pm.IdMedicament,
                        Medicament = new MedicamentDTO()
                        {
                            IdMedicament = pm.Medicament.IdMedicament,
                            Name = pm.Medicament.Name,
                            Description = pm.Medicament.Description,
                            Type = pm.Medicament.Type
                        },
                        Dose = pm.Dose,
                        Details = pm.Details
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync();

    }
}