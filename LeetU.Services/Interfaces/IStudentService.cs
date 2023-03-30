using LeetU.Models;

namespace LeetU.Services.Interfaces;

public interface IStudentService
{
    IEnumerable<Student> GetStudents(params long[] studentIds);
    IEnumerable<StudentWithCourses> GetStudentsWithCourses(params long[] studentIds);
    Task<int> SetStudentCourseAsync(long studentId, long courseId);
    bool HasCourse(long studentId, long courseId);
}