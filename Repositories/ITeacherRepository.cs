using StudentCoursesApi_NET8.Models;

namespace StudentCoursesApi_NET8.Repositories;

public interface ITeacherRepository
{
    Task<List<Teacher>> GetAllAsync();
    Task<Teacher?> GetByIdAsync(int id);
    Task<Teacher> AddAsync(Teacher teacher);
    Task<bool> UpdateAsync(Teacher teacher);
    Task<bool> DeleteAsync(int id);
}