using System.Collections;
using CodeFirst.Models;

namespace CodeFirst.DTOs;

public class PatientGetDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public virtual IEnumerable<PrescriptionGetDTO> Prescriptions { get; set; }
}

public class PrescriptionGetDTO
    {
        public int IdPrescription { get; set; }
        public DateOnly Date { get; set; }
        public DateOnly DueDate { get; set; }
        public virtual IEnumerable<MedicamentGetDTO> Medicaments { get; set; }
        public virtual DoctorGetDTO Doctor { get; set; }

    }

    public class MedicamentGetDTO
    {
        public int IdMedicament { get; set; }
        public string MedicamentName { get; set; }
        public int? Dose { get; set; }
        public string Details { get; set; }
 
    }

    public class DoctorGetDTO
    {
        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
    }
