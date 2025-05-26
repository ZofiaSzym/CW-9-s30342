using CodeFirst.Models;

namespace CodeFirst.DTOs;

public class PrescriptionCreateDTO
{
    
    public PatientGetCreateDTO Patient { get; set; }
    public DoctorGetDTO Doctor { get; set; }
    public IEnumerable<MedicamentGetDTO> Medicament { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }

}

public class PatientGetCreateDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
}