using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCoursesApi_NET8.DTOs;
using StudentCoursesApi_NET8.Models;
using StudentCoursesApi_NET8.Repositories;

namespace StudentCoursesApi_NET8.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CoursesController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;

    public CoursesController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetAll()
    {
        var courses = await _courseRepository.GetAllAsync();
        var result = courses.Select(c => new CourseReadDto
        {
            Id = c.Id,
            Title = c.Title,
            Credits = c.Credits,
            TeacherId = c.TeacherId,
            TeacherName = c.Teacher?.FullName
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<CourseReadDto>> GetById(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null) return NotFound();

        var dto = new CourseReadDto
        {
            Id = course.Id,
            Title = course.Title,
            Credits = course.Credits,
            TeacherId = course.TeacherId,
            TeacherName = course.Teacher?.FullName
        };

        return Ok(dto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CourseReadDto>> Create(CourseCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var course = new Course
        {
            Title = dto.Title,
            Credits = dto.Credits,
            TeacherId = dto.TeacherId
        };

        course = await _courseRepository.AddAsync(course);

        var readDto = new CourseReadDto
        {
            Id = course.Id,
            Title = course.Title,
            Credits = course.Credits,
            TeacherId = course.TeacherId
        };

        return CreatedAtAction(nameof(GetById), new { id = course.Id }, readDto);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, CourseUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var course = new Course
        {
            Id = id,
            Title = dto.Title,
            Credits = dto.Credits,
            TeacherId = dto.TeacherId
        };

        var success = await _courseRepository.UpdateAsync(course);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _courseRepository.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
