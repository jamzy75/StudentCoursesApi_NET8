using System.ComponentModel.DataAnnotations;

namespace StudentCoursesApi_NET8.Models;

public class Student
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FullName { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    public List<Enrollment> Enrollments { get; set; } = new();
}