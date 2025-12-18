using StudentCoursesApi_NET8.Models;

namespace StudentCoursesApi_NET8.Repositories;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<Course> AddAsync(Course course);
    Task<bool> UpdateAsync(Course course);
    Task<bool> DeleteAsync(int id);
}