using Microsoft.EntityFrameworkCore;
using StudentCoursesApi_NET8.Data;
using StudentCoursesApi_NET8.Models;

namespace StudentCoursesApi_NET8.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly ApplicationDbContext _context;

    public TeacherRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Teacher>> GetAllAsync()
    {
        return await _context.Teachers.AsNoTracking().ToListAsync();
    }

    public async Task<Teacher?> GetByIdAsync(int id)
    {
        return await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Teacher> AddAsync(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        return teacher;
    }

    public async Task<bool> UpdateAsync(Teacher teacher)
    {
        var exists = await _context.Teachers.AnyAsync(t => t.Id == teacher.Id);
        if (!exists) return false;

        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null) return false;

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();
        return true;
    }
}