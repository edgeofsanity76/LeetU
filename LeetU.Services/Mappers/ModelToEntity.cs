using LeetU.Models;

namespace LeetU.Services.Mappers;

/// <summary>
/// Entity to Model mapping. In another example I will use AutoMapper.
/// </summary>
internal static class ModelToEntity
{
    public static Data.Entities.Course CreateEntityFromCourse(Course course)
    {
        if (course == null)
            throw new ArgumentNullException(nameof(course));

        return new Data.Entities.Course()
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            StartDate = course.StartDate.ToShortDateString()
        };
    }

    public static Data.Entities.Course UpdateEntityFromCourse(Data.Entities.Course courseEntity, Course courseModel)
    {
        if (courseEntity == null)
            throw new ArgumentNullException(nameof(courseEntity));

        if (courseModel == null)
            throw new ArgumentNullException(nameof(courseModel));
            
        courseEntity.Name = courseModel.Name;
        courseEntity.Description = courseModel.Description;
        courseEntity.StartDate = courseModel.StartDate.ToShortDateString();
        return courseEntity;
    }

    // Create a student entity from a student model
    public static Data.Entities.Student CreateEntityFromStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));

        return new Data.Entities.Student()
        {
            Id = student.Id,
            Name = student.Name,
            Surname = student.Surname,
            DateOfBirth = student.DateOfBirth.ToShortDateString(),
            Sex = (long) student.Sex,
        };
    }
}