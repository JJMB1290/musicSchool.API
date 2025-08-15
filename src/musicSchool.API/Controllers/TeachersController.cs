using Microsoft.AspNetCore.Mvc;
using musicSchool.Application.DTOs;
using musicSchool.Infrastructure.Persistence;
using musicSchool.Domain.Entities;

namespace musicSchool.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly MusicSchoolDbContext _db;
    public TeachersController(MusicSchoolDbContext db) { _db = db; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeacherDto dto)
    {
        var teacher = new Teacher { Id = Guid.NewGuid(), Identifier = dto.Identifier, FirstName = dto.FirstName, LastName = dto.LastName, SchoolId = dto.SchoolId };
        _db.Teachers.Add(teacher);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var teacher = await _db.Teachers.FindAsync(id);
        if (teacher == null) return NotFound();
        return Ok(teacher);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeacherDto dto)
    {
        var teacher = await _db.Teachers.FindAsync(id);
        if (teacher == null) return NotFound();
        teacher.Identifier = dto.Identifier;
        teacher.FirstName = dto.FirstName;
        teacher.LastName = dto.LastName;
        teacher.SchoolId = dto.SchoolId;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var teacher = await _db.Teachers.FindAsync(id);
        if (teacher == null) return NotFound();
        _db.Teachers.Remove(teacher);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
