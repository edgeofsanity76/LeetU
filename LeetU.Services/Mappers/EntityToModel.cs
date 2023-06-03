using LeetU.Models;
using LeetU.Models.Interfaces;
using System.Globalization;
using Student = LeetU.Data.Entities.Student;

namespace LeetU.Services.Mappers;

/// <summary>
/// Entity to Model mapping. In another example I will use AutoMapper.
/// </summary>
internal static class EntityToModel
{
    public static Course CreateCourseFromEntity(Data.Entities.Course entity)
    {
        return new Course()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description ?? string.Empty,
            StartDate = DateTime.Parse(entity.StartDate ?? "01/01/1970")
        };
    }

    public static T CreateStudentFromEntity<T>(Student entity) where T : IStudent
    {
        var student = Activator.CreateInstance<T>();

        student.Id = entity.Id;
        student.Name = entity.Name;
        student.Surname = entity.Surname;
        student.DateOfBirth = DateTime.ParseExact(entity.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        student.Sex = (Sex)entity.Sex;
        student.Address = CreateAddressFromEntity(entity.Address);

        return student;
    }

    public static Address? CreateAddressFromEntity(Data.Entities.Address entity)
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
    public static IEnumerable<Course>? CreateCoursesFromEntities(IEnumerable<Data.Entities.Course> courses)
    {
        return courses.Select(CreateCourseFromEntity);
    }
}