using LeetU.Services.Exceptions;
using LeetU.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeetU.Controllers;

/// <summary>
/// The student controller. All controllers should have MINIMAL code (why do you think minimal apis exist). We take the request and then just perform an action on the service
/// Having minimal code reduces the need to test logic in these controllers, which is pretty tricky.
/// We COULD use MediatR pattern here, but in this example we are just keeping things simple. Google MediatR if you're curious
/// </summary>
[ApiController]
[Route("student")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [Route("{studentId}")]
    [HttpGet]
    public IActionResult GetStudent([FromRoute] long studentId)
    {
        var students = _studentService.GetStudents(studentId);
        var student = students.FirstOrDefault();

        return student == null ? new NotFoundResult() : new OkObjectResult(student);
    }

    [Route("")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var students = _studentService.GetStudents();
        return new OkObjectResult(students);
    }

    [Route("{studentId}/course")]
    [HttpGet]
    public IActionResult GetStudentWithCourses([FromRoute] long studentId)
    {
        var students = _studentService.GetStudentsWithCourses(studentId);
        return new OkObjectResult(students);
    }

    [Route("{studentId}/course/{courseId}")]
    [HttpPost]
    public async Task<IActionResult> SetStudentCourse([FromRoute] long studentId, [FromRoute] long courseId)
    {
        try
        {
            await _studentService.SetStudentCourseAsync(studentId, courseId);
        }
        catch (EntityNotFoundException e)
        {
            return new NotFoundObjectResult(e.Message);
        }
        
        return new OkResult();
    }
}