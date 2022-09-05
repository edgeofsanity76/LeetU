using LeetU.Models;

namespace LeetU.Services.Interfaces;

public interface ICourseService
{
    IEnumerable<Course> GetCourses(params long[] courseIds);
    Task<int> SetCourseAsync(Course course);
}