using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using musicSchool.Application.DTOs;
using musicSchool.Infrastructure.Persistence;
using musicSchool.Domain.Entities;

namespace musicSchool.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchoolsController : ControllerBase
{
    private readonly MusicSchoolDbContext _db;
    public SchoolsController(MusicSchoolDbContext db) { _db = db; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSchoolDto dto)
    {
        var school = new School { Id = Guid.NewGuid(), Code = dto.Code, Name = dto.Name, Description = dto.Description };
        _db.Schools.Add(school);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {

            return BadRequest();
        }
        
        return CreatedAtAction(nameof(GetById), new { id = school.Id }, school);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var school = await _db.Schools.FindAsync(id);
        if (school == null) return NotFound();
        return Ok(school);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSchoolDto dto)
    {
        var school = await _db.Schools.FindAsync(id);
        if (school == null) return NotFound();
        school.Code = dto.Code;
        school.Name = dto.Name;
        school.Description = dto.Description;
        try
        {
            await _db.SaveChangesAsync();
        }
        catch
        {

            return NoContent();
        }
        
        return Ok();
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var school = await _db.Schools.FindAsync(id);
        if (school == null) return NotFound();
        _db.Schools.Remove(school);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch
        {

            return NoContent(); 
        }

        return Ok("");
    }
}
