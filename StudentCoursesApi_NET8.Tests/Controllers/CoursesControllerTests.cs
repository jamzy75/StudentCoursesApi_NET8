using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentCoursesApi_NET8.Controllers;
using StudentCoursesApi_NET8.Models;
using StudentCoursesApi_NET8.Repositories;
using Xunit;

namespace StudentCoursesApi_NET8.Tests.Controllers;

public class CoursesControllerTests
{
    [Fact]
    public async Task GetAll_Returns_Ok_With_List()
    {
        var courses = new List<Course>
        {
            new() { Id = 1, Title = "Course A", Credits = 5, TeacherId = 1 },
            new() { Id = 2, Title = "Course B", Credits = 5, TeacherId = 1 }
        };

        var repoMock = new Mock<ICourseRepository>();
        repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(courses);

        var controller = new CoursesController(repoMock.Object);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<object>>(ok.Value!);
        Assert.Equal(2, list.Count());
    }
}