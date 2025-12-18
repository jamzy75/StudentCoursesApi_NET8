using System.ComponentModel.DataAnnotations;

namespace StudentCoursesApi_NET8.DTOs;

public class TeacherReadDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? Email { get; set; }
    public string? Department { get; set; }
}

public class TeacherCreateDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = default!;

    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(100)]
    public string? Department { get; set; }
}

public class TeacherUpdateDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = default!;

    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(100)]
    public string? Department { get; set; }
}