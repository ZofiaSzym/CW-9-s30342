﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models;

[Table("Medicament")]
public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
    [MaxLength(100)]
    public string Type { get; set; }
    
}