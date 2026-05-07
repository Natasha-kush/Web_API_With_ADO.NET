using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCoreWebAPICRUD.Models;

public partial class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? StudentName { get; set; }

    public string? StudentGender { get; set; }

    public int? Age { get; set; }

    public int? Standard { get; set; }

    public string? FatherName { get; set; }
}
