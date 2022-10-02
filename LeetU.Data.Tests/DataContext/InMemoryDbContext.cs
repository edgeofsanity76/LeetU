using System;
using LeetU.Data.Context;
using LeetU.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeetU.Data.Tests.DataContext;

/// <summary>
/// This is an in memory data context. This works exactly like a normal context would except its in memory. We use this for testing our repos and data services
/// As well as providing a data service for all other testable services.
/// </summary>
public class InMemoryDbContext : IDisposable
{
    public StudentContext StudentContext { get; private set; }

    public InMemoryDbContext()
    {
        Reset();
    }

    public void Reset()
    {
        CreateDatabase();
        SeedData(10);
    }

    public void CreateDatabase()
    {
        var options = new DbContextOptionsBuilder<StudentContext>().UseInMemoryDatabase("TestDatabase").Options;
        StudentContext = new StudentContext(options);
        StudentContext.Database.EnsureDeleted();
        StudentContext.Database.EnsureCreated();
    }

    public void SeedData(int numberOfRecords)
    {
        for (var record = 1; record < numberOfRecords + 1; record++)
        {
            StudentContext.Students.Add(new Student()
            {
                AddressId = record,
                Address = new Address()
                {
                    AddressLine1 = "TestAddressLine1",
                    AddressLine2 = "TestAddressLine2",
                    AddressLine3 = "TestAddressLine3",
                    County = "TestCounty",
                    Id = record,
                    PostCode = "TestPostCode",
                    Town = "TestTown"
                },
                DateOfBirth = "01/01/2020",
                Id = record,
                Name = "TestStudentName1",
                Sex = 1,
                Surname = "TestStudentSurname1"
            });

            StudentContext.Courses.Add(new Course()
            {
                Description = "TestCourse",
                Id = record,
                Name = "TestCourse",
                StartDate = "01/01/2020"
            });

            StudentContext.StudentCourses.Add(new StudentCourse()
            {
                CourseId = record,
                Id = record,
                StudentId = record
            });
        }

        StudentContext.SaveChanges();
    }

    public void Dispose()
    {
        StudentContext.Dispose();
    }
}