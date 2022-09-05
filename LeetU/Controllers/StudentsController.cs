using LeetU.Services.Exceptions;
using LeetU.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeetU.Controllers;

[ApiController]
[Route("students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
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
    public IActionResult GetAllStudents()
    {
        var students = _studentService.GetStudents();
        return new OkObjectResult(students);
    }

    [Route("{studentId}/courses")]
    [HttpGet]
    public IActionResult GetStudentWithCourses([FromRoute] long studentId)
    {
        var students = _studentService.GetStudentsWithCourses(studentId);
        return new OkObjectResult(students);
    }

    [Route("{studentId}/courses/{courseId}")]
    [HttpPost]
    public async Task<IActionResult> SetStudentCourse([FromRoute] long studentId, [FromRoute] long courseId)
    {
        try
        {
            await _studentService.SetStudentCourseAsync(studentId, courseId);
        }
        catch (EntityNotFoundException e)
        {
            return new JsonResult(new NotFoundObjectResult(e.Message));
        }
        
        return new OkResult();
    }
}