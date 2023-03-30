using LeetU.Data.Context;
using LeetU.Data.Entities;
using LeetU.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeetU.Data.Repositories;

/// <summary>
/// WHY do you ask. WHY is this empty? Well, if there any repository operations that are specialised, they go here and not in the CRUD base
/// For example, if using Stored Procedures, the code to access them would be here.
/// </summary>
public class StudentCourseRepository : RepositoryCrud<StudentCourse>, IStudentCourseRepositoryCrud
{
    private readonly StudentContext _context;

    public StudentCourseRepository(StudentContext context) : base(context)
    {
        _context = context;
    }

    public bool HasCourse(long studentId, long courseId)
    {
        var student = _context.Students.Include("StudentCourses").FirstOrDefault(s => s.Id == studentId);
        return student != null && student.StudentCourses.Any(c => c.CourseId == courseId);
    }
}