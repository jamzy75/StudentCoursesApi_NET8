using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentCoursesApi_NET8.Controllers;
using StudentCoursesApi_NET8.DTOs;
using StudentCoursesApi_NET8.Models;
using StudentCoursesApi_NET8.Repositories;
using Xunit;

namespace StudentCoursesApi_NET8.Tests.Controllers;

public class StudentsControllerTests
{
    [Fact]
    public async Task GetAll_Returns_Ok_With_ListOfDtos()
    {
        var students = new List<Student>
        {
            new() { Id = 1, FullName = "Alice Test", Email = "alice@test.com" },
            new() { Id = 2, FullName = "Bob Test",   Email = "bob@test.com" }
        };

        var repoMock = new Mock<IStudentRepository>();
        repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(students);

        var controller = new StudentsController(repoMock.Object);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var dtos = Assert.IsAssignableFrom<IEnumerable<StudentReadDto>>(ok.Value);

        Assert.Equal(2, dtos.Count());
        Assert.Contains(dtos, d => d.FullName == "Alice Test");
    }

    [Fact]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        var repoMock = new Mock<IStudentRepository>();
        repoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync((Student?)null);

        var controller = new StudentsController(repoMock.Object);

        var result = await controller.GetById(10);

        Assert.IsType<NotFoundResult>(result.Result);
    }
}