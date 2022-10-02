using LeetU.Models;

namespace LeetU.Services.Mappers
{
    /// <summary>
    /// Model to Entity mapping
    /// </summary>
    internal static class ModelToEntity
    {
        public static Data.Entites.Course CreateEntityFromCourse(Course course)
        {
            return new Data.Entites.Course()
            {
                Name = course.Name,
                Description = course.Description,
                StartDate = course.StartDate.ToShortDateString()
            };
        }
    }
}
