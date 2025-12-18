using Microsoft.EntityFrameworkCore;
using StudentCoursesApi_NET8.Data;
using StudentCoursesApi_NET8.Models;

namespace StudentCoursesApi_NET8.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students.AsNoTracking().ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student> AddAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<bool> UpdateAsync(Student student)
    {
        var exists = await _context.Students.AnyAsync(s => s.Id == student.Id);
        if (!exists) return false;

        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }
}