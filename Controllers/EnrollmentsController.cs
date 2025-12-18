using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCoursesApi_NET8.DTOs;
using StudentCoursesApi_NET8.Models;
using StudentCoursesApi_NET8.Repositories;

namespace StudentCoursesApi_NET8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentsController(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<EnrollmentReadDto>>> GetAll()
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        var result = enrollments.Select(e => new EnrollmentReadDto
        {
            Id = e.Id,
            StudentId = e.StudentId,
            StudentName = e.Student?.FullName,
            CourseId = e.CourseId,
            CourseTitle = e.Course?.Title,
            EnrolledOn = e.EnrolledOn
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<EnrollmentReadDto>> GetById(int id)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id);
        if (enrollment == null) return NotFound();

        var dto = new EnrollmentReadDto
        {
            Id = enrollment.Id,
            StudentId = enrollment.StudentId,
            StudentName = enrollment.Student?.FullName,
            CourseId = enrollment.CourseId,
            CourseTitle = enrollment.Course?.Title,
            EnrolledOn = enrollment.EnrolledOn
        };

        return Ok(dto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<EnrollmentReadDto>> Create(EnrollmentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var enrollment = new Enrollment
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId
        };

        enrollment = await _enrollmentRepository.AddAsync(enrollment);

        var readDto = new EnrollmentReadDto
        {
            Id = enrollment.Id,
            StudentId = enrollment.StudentId,
            CourseId = enrollment.CourseId,
            EnrolledOn = enrollment.EnrolledOn
        };

        return CreatedAtAction(nameof(GetById), new { id = enrollment.Id }, readDto);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, EnrollmentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var enrollment = new Enrollment
        {
            Id = id,
            StudentId = dto.StudentId,
            CourseId = dto.CourseId
        };

        var success = await _enrollmentRepository.UpdateAsync(enrollment);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _enrollmentRepository.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
