using CodeFirst.DTOs;
using CodeFirst.Exception;
using CodeFirst.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController(IDbService service):ControllerBase
{
 [HttpPost]
 public async Task<ActionResult> CreatePrescription([FromBody] PrescriptionCreateDTO prescription)
 {
    try
    { 
       await service.CreatePrescriptionAsync(prescription);
       return NoContent();
    }
    catch (NotFoundException e)
    {
        return NotFound(e.Message);
    }
 }
}