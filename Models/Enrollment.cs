using System.ComponentModel.DataAnnotations;

namespace StudentCoursesApi_NET8.Models;

public class Enrollment
{
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; }
    public Student? Student { get; set; }

    [Required]
    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public DateTime EnrolledOn { get; set; } = DateTime.UtcNow;
}