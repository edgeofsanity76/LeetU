using LeetU.Models;
using LeetU.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeetU.Controllers;

[ApiController]
[Route("courses")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [Route("")]
    [HttpGet]
    public IActionResult GetAllCourses()
    {
        var courses = _courseService.GetCourses();
        return new OkObjectResult(courses);
    }

    [Route("{courseId}")]
    [HttpGet]
    public IActionResult GetCourse([FromRoute] long courseId)
    {
        var courses = _courseService.GetCourses(courseId);
        var course = courses.FirstOrDefault();

        return course == null ? new NotFoundResult() : new OkObjectResult(course);
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> SetCourse([FromBody] Course course)
    {
        var rowsAffected = await _courseService.SetCourseAsync(course);

        if (rowsAffected > 0)
            return new OkResult();

        throw new Exception("There was an error creating the course");
    }
}