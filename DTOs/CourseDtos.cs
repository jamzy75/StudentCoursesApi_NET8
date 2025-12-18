using System.ComponentModel.DataAnnotations;

namespace StudentCoursesApi_NET8.DTOs;

public class CourseReadDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int Credits { get; set; }
    public int TeacherId { get; set; }
    public string? TeacherName { get; set; }
}

public class CourseCreateDto
{
    [Required, MaxLength(100)]
    public string Title { get; set; } = default!;

    public int Credits { get; set; }

    [Required]
    public int TeacherId { get; set; }
}

public class CourseUpdateDto
{
    [Required, MaxLength(100)]
    public string Title { get; set; } = default!;

    public int Credits { get; set; }

    [Required]
    public int TeacherId { get; set; }
}