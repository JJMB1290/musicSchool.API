using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using musicSchool.Application.DTOs;
using musicSchool.Infrastructure.Persistence;

namespace musicSchool.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly MusicSchoolDbContext _db;
    public AssignmentsController(MusicSchoolDbContext db) { _db = db; }

    [HttpPost("assign-student-teacher")]
    public async Task<IActionResult> AssignStudentToTeacher([FromBody] AssignStudentToTeacherDto dto)
    {
        _db.StudentTeachers.Add(new Domain.Entities.StudentTeacher { StudentId = dto.StudentId, TeacherId = dto.TeacherId });
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("enroll-student-school")]
    public async Task<IActionResult> EnrollStudentToSchool([FromBody] EnrollStudentToSchoolDto dto)
    {
        _db.StudentSchools.Add(new Domain.Entities.StudentSchool { StudentId = dto.StudentId, SchoolId = dto.SchoolId });
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("students-by-teacher/{teacherId}")]
    public async Task<IActionResult> GetStudentsByTeacher(Guid teacherId)
    {
        var results = await (from st in _db.StudentTeachers
                             join s in _db.Students on st.StudentId equals s.Id
                             join t in _db.Teachers on st.TeacherId equals t.Id
                             join sc in _db.Schools on t.SchoolId equals sc.Id
                             where t.Id == teacherId
                             select new { s.Id, s.Identifier, s.FirstName, s.LastName, SchoolName = sc.Name })
                            .ToListAsync();
        return Ok(results);
    }

    [HttpGet("schools-and-students-by-teacher/{teacherId}")]
    public async Task<IActionResult> GetSchoolsAndStudentsByTeacher(Guid teacherId)
    {
        var results = await (from t in _db.Teachers
                             join sc in _db.Schools on t.SchoolId equals sc.Id
                             join st in _db.StudentTeachers on t.Id equals st.TeacherId into stg
                             from st in stg.DefaultIfEmpty()
                             join s in _db.Students on st.StudentId equals s.Id into sg
                             from s in sg.DefaultIfEmpty()
                             where t.Id == teacherId
                             select new { sc.Id, sc.Name, StudentId = s != null ? s.Id : Guid.Empty, StudentFirstName = s != null ? s.FirstName : null, StudentLastName = s != null ? s.LastName : null })
                            .ToListAsync();
        return Ok(results);
    }
}
