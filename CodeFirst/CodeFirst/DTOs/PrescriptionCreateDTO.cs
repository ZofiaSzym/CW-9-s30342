using CodeFirst.Models;

namespace CodeFirst.DTOs;

public class PrescriptionCreateDTO
{
    public Patient Patient { get; set; }
    public MedicamentGetDTO Medicament { get; set; }
    public Doctor Doctor { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
}