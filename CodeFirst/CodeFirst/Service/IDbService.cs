using CodeFirst.Data;
using CodeFirst.DTOs;
using CodeFirst.Exception;
using CodeFirst.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Service;

public interface IDbService
{
    public Task<PatientGetDTO> GetPatientDetailsByIdAsync(int patientId);
    public Task CreatePrescriptionAsync(PrescriptionCreateDTO prescriptionData);
    
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PatientGetDTO> GetPatientDetailsByIdAsync(int patientId)
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
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescription
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionGetDTO
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorGetDTO
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentGetDTO
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        MedicamentName = pm.Medicament.Name,
                        Dose =  pm.Dose,
                        Details = pm.Details
                    }).ToList()
                }).ToList()
        };

        return response;
    }

    public async Task CreatePrescriptionAsync(PrescriptionCreateDTO prescriptionData)
    {
        List<Medicament> meds = [];

        if (prescriptionData.Medicament.Count() > 10)
        {
            throw new TooMuchException("You can't add more than 10 medicaments");
        }
        
        if (prescriptionData.Medicament is not null && prescriptionData.Medicament.Count() != 0)
        {
            foreach (var idMedicament in prescriptionData.Medicament)
            {
                var med = await data.Medicaments.FirstOrDefaultAsync(m => m.IdMedicament == idMedicament.IdMedicament);
                if (med is null)
                {
                    throw new NotFoundException($"Medicament with id: {med.IdMedicament} not found");
                }
                meds.Add(med);
            }
        }
        
        var patientData = prescriptionData.Patient;
        var patient = await data.Patients.FirstOrDefaultAsync(p =>
            p.FirstName == patientData.FirstName &&
            p.LastName == patientData.LastName &&
            p.Birthdate == patientData.Birthdate
        );

        if (patient is null)
        {
            patient = new Patient
            {
                FirstName = patientData.FirstName,
                LastName = patientData.LastName,
                Birthdate = patientData.Birthdate,
            };
            await data.Patients.AddAsync(patient);
            await data.SaveChangesAsync(); 
        }

        if (prescriptionData.DueDate < prescriptionData.Date)
        {
            throw new TooLateException("Due date cannot be before prescription date");
        }

        var prescription = new Prescription
        {
            Date = prescriptionData.Date,
            DueDate = prescriptionData.DueDate,
            IdDoctor = prescriptionData.Doctor.IdDoctor,
            IdPatient = patient.IdPatient,
        };
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();

        foreach (var med in prescriptionData.Medicament)
        {
            var m = new PrescriptionMedicament
            {
                IdMedicament = med.IdMedicament,
                IdPrescription = prescription.IdPrescription,
                Dose = med.Dose,
                Details = med.Details
            };
            await data.PrescriptionMedicaments.AddAsync(m);
        }
        await data.SaveChangesAsync();
      
        
    }
    
}