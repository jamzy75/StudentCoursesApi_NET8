using System.ComponentModel.DataAnnotations;

namespace StudentCoursesApi_NET8.DTOs;

public class EnrollmentReadDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public int CourseId { get; set; }
    public string? CourseTitle { get; set; }
    public DateTime EnrolledOn { get; set; }
}

public class EnrollmentCreateDto
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    public int CourseId { get; set; }
}