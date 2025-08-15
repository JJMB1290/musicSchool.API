using System;

namespace musicSchool.Domain.Entities;

public class StudentTeacher
{
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }

    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
}
