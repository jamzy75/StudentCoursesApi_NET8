using System.ComponentModel.DataAnnotations;

namespace StudentCoursesApi_NET8.Models;

public class Teacher
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FullName { get; set; } = default!;

    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(100)]
    public string? Department { get; set; }

    public List<Course> Courses { get; set; } = new();
}