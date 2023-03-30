using LeetU.Data.Entities;

namespace LeetU.Data.Interfaces;

public interface IStudentCourseRepositoryCrud : IRepositoryCrud<StudentCourse>
{
    /// <summary>
    /// Returns true if a student is part of a course
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="courseId"></param>
    /// <returns></returns>
    bool HasCourse(long studentId, long courseId);
}