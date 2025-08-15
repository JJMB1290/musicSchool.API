using System;
using System.Collections.Generic;

namespace musicSchool.Domain.Entities;

public class School
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    public ICollection<StudentSchool> StudentSchools { get; set; } = new List<StudentSchool>();
}
