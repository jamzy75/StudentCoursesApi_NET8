using System.ComponentModel.DataAnnotations;

namespace StudentCoursesApi_NET8.Models;

public class Course
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; } = default!;

    public int Credits { get; set; }

    public int TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public List<Enrollment> Enrollments { get; set; } = new();
}