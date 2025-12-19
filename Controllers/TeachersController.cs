using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCoursesApi_NET8.DTOs;
using StudentCoursesApi_NET8.Models;
using StudentCoursesApi_NET8.Repositories;

namespace StudentCoursesApi_NET8.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TeachersController : ControllerBase
{
    private readonly ITeacherRepository _teacherRepository;

    public TeachersController(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<TeacherReadDto>>> GetAll()
    {
        var teachers = await _teacherRepository.GetAllAsync();
        var result = teachers.Select(t => new TeacherReadDto
        {
            Id = t.Id,
            FullName = t.FullName,
            Email = t.Email,
            Department = t.Department
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<TeacherReadDto>> GetById(int id)
    {
        var teacher = await _teacherRepository.GetByIdAsync(id);
        if (teacher == null) return NotFound();

        var dto = new TeacherReadDto
        {
            Id = teacher.Id,
            FullName = teacher.FullName,
            Email = teacher.Email,
            Department = teacher.Department
        };

        return Ok(dto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TeacherReadDto>> Create(TeacherCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var teacher = new Teacher
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Department = dto.Department
        };

        teacher = await _teacherRepository.AddAsync(teacher);

        var readDto = new TeacherReadDto
        {
            Id = teacher.Id,
            FullName = teacher.FullName,
            Email = teacher.Email,
            Department = teacher.Department
        };

        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, readDto);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, TeacherUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var teacher = new Teacher
        {
            Id = id,
            FullName = dto.FullName,
            Email = dto.Email,
            Department = dto.Department
        };

        var success = await _teacherRepository.UpdateAsync(teacher);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _teacherRepository.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
