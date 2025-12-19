using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentCoursesApi_NET8.Data;
using StudentCoursesApi_NET8.Models;
using StudentCoursesApi_NET8.Repositories;
using Xunit;

namespace StudentCoursesApi_NET8.Tests.Repositories;

public class StudentRepositoryTests
{
    private ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"StudentsDb_{System.Guid.NewGuid()}")
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task AddAsync_Saves_Student()
    {
        await using var context = CreateDbContext();
        var repo = new StudentRepository(context);

        var student = new Student
        {
            FullName = "Alice Test",
            Email = "alice@test.com"
        };

        var saved = await repo.AddAsync(student);
        var all = await repo.GetAllAsync();

        Assert.NotEqual(0, saved.Id);
        Assert.Single(all);
        Assert.Equal("Alice Test", all.First().FullName);
    }

    [Fact]
    public async Task GetByIdAsync_Returns_Null_When_NotFound()
    {
        await using var context = CreateDbContext();
        var repo = new StudentRepository(context);

        var result = await repo.GetByIdAsync(999);

        Assert.Null(result);
    }
}