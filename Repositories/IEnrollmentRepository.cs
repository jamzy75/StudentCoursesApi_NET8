using StudentCoursesApi_NET8.Models;

namespace StudentCoursesApi_NET8.Repositories;

public interface IEnrollmentRepository
{
    Task<List<Enrollment>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(int id);
    Task<Enrollment> AddAsync(Enrollment enrollment);
    Task<bool> UpdateAsync(Enrollment enrollment);
    Task<bool> DeleteAsync(int id);
}