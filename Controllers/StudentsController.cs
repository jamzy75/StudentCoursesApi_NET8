using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCoursesApi_NET8.DTOs;
using StudentCoursesApi_NET8.Models;
using StudentCoursesApi_NET8.Repositories;

namespace StudentCoursesApi_NET8.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

    public StudentsController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetAll()
    {
        var students = await _studentRepository.GetAllAsync();
        var result = students.Select(s => new StudentReadDto
        {
            Id = s.Id,
            FullName = s.FullName,
            Email = s.Email
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<StudentReadDto>> GetById(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null) return NotFound();

        var dto = new StudentReadDto
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email
        };

        return Ok(dto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<StudentReadDto>> Create(StudentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var student = new Student
        {
            FullName = dto.FullName,
            Email = dto.Email
        };

        student = await _studentRepository.AddAsync(student);

        var readDto = new StudentReadDto
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email
        };

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, readDto);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, StudentUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var student = new Student
        {
            Id = id,
            FullName = dto.FullName,
            Email = dto.Email
        };

        var success = await _studentRepository.UpdateAsync(student);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _studentRepository.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
