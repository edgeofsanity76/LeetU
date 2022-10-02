using LeetU.Models;
using LeetU.Models.Interfaces;

namespace LeetU.Services.Mappers
{
    /// <summary>
    /// Entity to Model mapping
    /// </summary>
    internal static class EntityToModel
    {
        public static Course CreateCourseFromEntity(LeetU.Data.Entites.Course entity)
        {
            return new Course()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description ?? string.Empty,
                StartDate = DateTime.Parse(entity.StartDate ?? "01/01/1970")
            };
        }

        public static T CreateStudentFromEntity<T>(LeetU.Data.Entites.Student entity) where T : IStudent
        {
            var student = Activator.CreateInstance<T>();

            student.Id = entity.Id;
            student.FirstName = entity.Name;
            student.Surname = entity.Surname;
            student.DateOfBirth = DateTime.Parse(entity.DateOfBirth);
            student.Sex = (Sex)entity.Sex;
            student.Address = CreateAddressFromEntity(entity.Address);

            return student;
        }

        public static Address CreateAddressFromEntity(LeetU.Data.Entites.Address entity)
        {
            return new Address()
            {
                Id = entity.Id,
                AddressLine1 = entity?.AddressLine1 ?? string.Empty,
                AddressLine2 = entity?.AddressLine2 ?? string.Empty,
                AddressLine3 = entity?.AddressLine3 ?? string.Empty,
                County = entity?.County ?? string.Empty,
                Town = entity?.Town ?? string.Empty,
                PostCode = entity?.PostCode ?? string.Empty
            };
        }
        public static IEnumerable<Course> CreateCoursesFromEntities(IEnumerable<LeetU.Data.Entites.Course> courses)
        {
            return courses.Select(CreateCourseFromEntity);
        }
    }
}
