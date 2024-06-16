using APBD11.Entities;
using APBD11.DTOs;
using APBD11.Exceptions;
using APBD11.Repositories;
using APBD11.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD11.Controllers;

[ApiController]
[Route("/prescriptionapp")]
public class PrescriptionAppController : ControllerBase
{
    private IPrescriptionService _prescriptionService;
    private IGetPatientService _getPatientService;
    private IUserService _userService;

    public PrescriptionAppController(IPrescriptionService prescriptionService, 
        IGetPatientService getPatientService, IUserService userService)
    {
        _prescriptionService = prescriptionService;
        _getPatientService = getPatientService;
        _userService = userService;
    }
    [HttpPost]
    public async Task<IActionResult> CreatePrescription(PrescriptionPostDTO prescriptionPostDto)
    {
        try
        {
            await _prescriptionService.AddPrescription(prescriptionPostDto);
        }
        catch (DomainException e)
        {
            return BadRequest(new { message = e.Message });
        }

        return Ok("Success");
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatientInformation(int id)
    {
        PatientWithPrescriptionsDTO patient;
        try
        {
            patient = await _getPatientService.GetPatient(id);
        }
        catch (DomainException e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
        return Ok(patient);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        await _userService.RegisterUser(model);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _userService.LoginUser(loginRequest);

        return Ok(result);
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
    {
        var result = await _userService.RefreshUserToken(refreshToken);
        return Ok(result);
    }
}