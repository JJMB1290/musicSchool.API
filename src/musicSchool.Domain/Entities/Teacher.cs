using System;
using System.Collections.Generic;

namespace musicSchool.Domain.Entities;

public class Teacher
{
    public Guid Id { get; set; }
    public string Identifier { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public Guid SchoolId { get; set; }
    public School? School { get; set; }

    public ICollection<StudentTeacher> StudentTeachers { get; set; } = new List<StudentTeacher>();
}
