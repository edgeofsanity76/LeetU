using System;
using Xunit;

namespace LeetU.Services.Tests;

public class ModelToEntityTests
{
    [Fact]
    public void ShouldUpdateCourseFromModel()
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

        var updatedCourseEntity = Mappers.EntityToModel.UpdateCourseFromModel(courseEntity, courseModel);
        Assert.Equal(courseEntity.Id, updatedCourseEntity.Id);
        Assert.Equal(courseModel.Name, updatedCourseEntity.Name);
        Assert.Equal(courseModel.Description, updatedCourseEntity.Description);
        Assert.Equal(courseModel.StartDate.ToShortDateString(), updatedCourseEntity.StartDate);
    }

    [Fact]
    public void ShouldCreateCourseFromModel()
    {
        var courseModel = new Models.Course()
        {
            Id = 1,
            Name = "Test Course",
            Description = "Test Description",
            StartDate = DateTime.Parse("01/01/2021")
        };
        var courseEntity = Mappers.EntityToModel.CreateCourseFromModel(courseModel);
        Assert.Equal(courseModel.Id, courseEntity.Id);
        Assert.Equal(courseModel.Name, courseEntity.Name);
        Assert.Equal(courseModel.Description, courseEntity.Description);
        Assert.Equal(courseModel.StartDate.ToShortDateString(), courseEntity.StartDate);
    }
}