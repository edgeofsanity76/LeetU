using LeetU.Models;

namespace LeetU.Services.Mappers
{
    /// <summary>
    /// Entity to Model mapping. In another example I will use AutoMapper.
    /// </summary>
    internal static class ModelToEntity
    {
        public static Data.Entities.Course CreateEntityFromCourse(Course course)
        {
            return new Data.Entities.Course()
            {
                Name = course.Name,
                Description = course.Description,
                StartDate = course.StartDate.ToShortDateString()
            };
        }
    }
}
