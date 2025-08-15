using Microsoft.AspNetCore.Mvc;
using musicSchool.Application.DTOs;
using musicSchool.Infrastructure.Persistence;
using musicSchool.Domain.Entities;

namespace musicSchool.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly MusicSchoolDbContext _db;
    public StudentsController(MusicSchoolDbContext db) { _db = db; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
    {
        var student = new Student { Id = Guid.NewGuid(), Identifier = dto.Identifier, FirstName = dto.FirstName, LastName = dto.LastName, DateOfBirth = dto.DateOfBirth };
        _db.Students.Add(student);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var student = await _db.Students.FindAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudentDto dto)
    {
        var student = await _db.Students.FindAsync(id);
        if (student == null) return NotFound();
        student.Identifier = dto.Identifier;
        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.DateOfBirth = dto.DateOfBirth;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await _db.Students.FindAsync(id);
        if (student == null) return NotFound();
        _db.Students.Remove(student);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
