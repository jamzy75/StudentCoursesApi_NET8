using System.ComponentModel.DataAnnotations;

namespace StudentCoursesApi_NET8.DTOs;

public class StudentReadDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
}

public class StudentCreateDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;
}

public class StudentUpdateDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;
}