using CodeFirst.Data;
using CodeFirst.DTOs;
using CodeFirst.Exception;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Service;

public interface IDbService
{
    public Task<PatientGetDTO> GetsPatientDetailsByIdAsync(int patientId);
    
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PatientGetDTO> GetsPatientDetailsByIdAsync(int patientId)
    {
        var patient = await data.Patients
            .Include(p => p.Prescription)
            .ThenInclude(p => p.Doctor)
            .Include(p => p.Prescription)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == patientId);

        if (patient == null)
           throw new NotFoundException("no patient found");

        var response = new PatientGetDTO
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Prescriptions = patient.Prescription
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionGetDTO
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctors = new DoctorGetDTO
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentGetDTO
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        MedicamentName = pm.Medicament.Name,
                        Dose =  pm.Dose,
                        Description = pm.Details
                    }).ToList()
                }).ToList()
        };

        return response;
    }
    
}