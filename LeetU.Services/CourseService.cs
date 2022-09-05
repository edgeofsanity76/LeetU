using LeetU.Data.Interfaces;
using LeetU.Models;
using LeetU.Services.Interfaces;
using LeetU.Services.Mappers;

namespace LeetU.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public IEnumerable<Course> GetCourses(params long[] courseIds)
    {
        var courses = _courseRepository.Get(c => courseIds.Any(id => c.Id == id) || courseIds.Length == 0);

        foreach (var entity in courses)
            yield return EntityToModel.CreateCourseFromEntity(entity);
    }

    public async Task<int> SetCourseAsync(Course course)
    {
        var entity = ModelToEntity.CreateEntityFromCourse(course);
        await _courseRepository.InsertAsync(entity);
        var rowsAffected = await _courseRepository.SaveChanges();
        return rowsAffected;
    }
}