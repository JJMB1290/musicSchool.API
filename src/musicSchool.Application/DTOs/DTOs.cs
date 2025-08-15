using System;

namespace musicSchool.Application.DTOs;

public record CreateSchoolDto(string Code, string Name, string? Description);
public record UpdateSchoolDto(Guid Id, string Code, string Name, string? Description);

public record CreateTeacherDto(string Identifier, string FirstName, string LastName, Guid SchoolId);
public record UpdateTeacherDto(Guid Id, string Identifier, string FirstName, string LastName, Guid SchoolId);

public record CreateStudentDto(string Identifier, string FirstName, string LastName, DateOnly DateOfBirth);
public record UpdateStudentDto(Guid Id, string Identifier, string FirstName, string LastName, DateOnly DateOfBirth);

public record AssignStudentToTeacherDto(Guid StudentId, Guid TeacherId);
public record EnrollStudentToSchoolDto(Guid StudentId, Guid SchoolId);
