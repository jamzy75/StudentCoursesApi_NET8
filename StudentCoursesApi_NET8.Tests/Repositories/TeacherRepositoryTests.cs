using Microsoft.EntityFrameworkCore;
using StudentCoursesApi_NET8.Data;
using StudentCoursesApi_NET8.Models;
using StudentCoursesApi_NET8.Repositories;
using Xunit;

namespace StudentCoursesApi_NET8.Tests.Repositories;

public class TeacherRepositoryTests
{
    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"teachers_db_{Guid.NewGuid()}")
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task AddAsync_Saves_Teacher()
    {
        using var context = CreateDbContext();
        var repo = new TeacherRepository(context);

        var teacher = new Teacher
        {
            FullName = "Dr. Smith",
            Email = "smith@example.com"
        };

        await repo.AddAsync(teacher);

        Assert.NotEqual(0, teacher.Id);
        Assert.Single(context.Teachers);
    }

    [Fact]
    public async Task GetByIdAsync_Returns_Teacher()
    {
        using var context = CreateDbContext();
        var teacher = new Teacher
        {
            FullName = "Dr. Jones",
            Email = "jones@example.com"
        };
        context.Teachers.Add(teacher);
        await context.SaveChangesAsync();

        var repo = new TeacherRepository(context);

        var result = await repo.GetByIdAsync(teacher.Id);

        Assert.NotNull(result);
        Assert.Equal("Dr. Jones", result!.FullName);
    }
}