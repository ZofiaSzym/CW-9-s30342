using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Data;

public class  AppDbContext: DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var medi = new Medicament
        {
            Description = "very healthy",
            IdMedicament = 1,
            Name = "allegra",
            Type = "anti-allergy"
            
        };

        var doctor = new Doctor
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@gmail.com",
            IdDoctor = 1
        };
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Paul",
            DateOfBirth = new DateOnly(1980, 12, 31),
            IdPatient = 1
        };
        var prescription = new Prescription
        {
            Date = new DateOnly(1980, 12, 31),
            DueDate = new DateOnly(1980, 12, 31),
            IdPrescription = 1,
            IdDoctor = 1,
            IdPatient = 1,
        };

        var pm = new PrescriptionMedicament
        {
            Details = "no comment",
            IdPrescription = 1,
            IdMedicament = 1,
            Dose = 20
        };
        modelBuilder.Entity<Medicament>().HasData([medi]);
        modelBuilder.Entity<Doctor>().HasData([doctor]);
        modelBuilder.Entity<Patient>().HasData([patient]);
        modelBuilder.Entity<Prescription>().HasData([prescription]);
        modelBuilder.Entity<PrescriptionMedicament>().HasData([pm]);
    }
}