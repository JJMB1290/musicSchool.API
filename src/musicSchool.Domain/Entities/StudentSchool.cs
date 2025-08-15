using System;

namespace musicSchool.Domain.Entities;

public class StudentSchool
{
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }

    public Guid SchoolId { get; set; }
    public School? School { get; set; }
}
