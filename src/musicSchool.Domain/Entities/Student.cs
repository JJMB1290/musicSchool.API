using System;
using System.Collections.Generic;

namespace musicSchool.Domain.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string Identifier { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }

    public ICollection<StudentTeacher> StudentTeachers { get; set; } = new List<StudentTeacher>();
    public ICollection<StudentSchool> StudentSchools { get; set; } = new List<StudentSchool>();
}
