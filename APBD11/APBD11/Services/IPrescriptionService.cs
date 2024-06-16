using APBD11.DTOs;

namespace APBD11.UseCases;

public interface IPrescriptionService
{
    Task<bool> AddPrescription(PrescriptionPostDTO prescriptionPostDto);
}