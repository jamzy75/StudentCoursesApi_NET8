using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentCoursesApi_NET8.Controllers;
using StudentCoursesApi_NET8.Models;
using StudentCoursesApi_NET8.Repositories;
using Xunit;

namespace StudentCoursesApi_NET8.Tests.Controllers;

public class TeachersControllerTests
{
    [Fact]
    public async Task GetAll_Returns_Ok_With_List()
    {
        var teachers = new List<Teacher>
        {
            new() { Id = 1, FullName = "Dr. Smith", Email = "smith@example.com" },
            new() { Id = 2, FullName = "Dr. Jones", Email = "jones@example.com" }
        };

        var repoMock = new Mock<ITeacherRepository>();
        repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(teachers);

        var controller = new TeachersController(repoMock.Object);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<object>>(ok.Value!);
        Assert.Equal(2, list.Count());
    }
}