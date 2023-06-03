using System;
using Xunit;

namespace LeetU.Services.Tests;

public class EntityToModelTests
{
    [Fact]
    public void ShouldCreateCourseFromEntity()
    {
        var courseEntity = new Data.Entities.Course()
        {
            Id = 1,
            Name = "Test Course",
            Description = "Test Description",
            StartDate = "01/01/2021"
        };

        var courseModel = Mappers.EntityToModel.CreateCourseFromEntity(courseEntity);

        Assert.Equal(courseEntity.Id, courseModel.Id);
        Assert.Equal(courseEntity.Name, courseModel.Name);
        Assert.Equal(courseEntity.Description, courseModel.Description);
        Assert.Equal(courseEntity.StartDate, courseModel.StartDate.ToShortDateString());
    }

    [Fact]
    public void ShouldUpdateCourseFromEntity()
    {
        var courseEntity = new Data.Entities.Course()
        {
            Id = 1,
            Name = "Test Course",
            Description = "Test Description",
            StartDate = "01/01/2021"
        };
        var courseModel = new Models.Course()
        {
            Id = 1,
            Name = "Test Course Updated",
            Description = "Test Description Updated",
            StartDate = DateTime.Parse("01/01/2021")
        };

        var updatedCourseEntity = Mappers.EntityToModel.UpdateCourseFromEntity(courseEntity, courseModel);
        Assert.Equal(courseEntity.Id, updatedCourseEntity.Id);
        Assert.Equal(courseModel.Name, updatedCourseEntity.Name);
        Assert.Equal(courseModel.Description, updatedCourseEntity.Description);
        Assert.Equal(courseModel.StartDate.ToShortDateString(), updatedCourseEntity.StartDate.ToShortDateString());
    }
}