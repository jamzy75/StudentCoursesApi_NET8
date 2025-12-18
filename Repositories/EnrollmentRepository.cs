using Microsoft.EntityFrameworkCore;
using StudentCoursesApi_NET8.Data;
using StudentCoursesApi_NET8.Models;

namespace StudentCoursesApi_NET8.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context;

    public EnrollmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Enrollment>> GetAllAsync()
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Enrollment?> GetByIdAsync(int id)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Enrollment> AddAsync(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<bool> UpdateAsync(Enrollment enrollment)
    {
        var exists = await _context.Enrollments.AnyAsync(e => e.Id == enrollment.Id);
        if (!exists) return false;

        _context.Enrollments.Update(enrollment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null) return false;

        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
        return true;
    }
}