using System.Linq;
using LeetU.Data.Repositories;
using LeetU.Data.Tests.DataContext;
using Xunit;
// ReSharper disable PossibleMultipleEnumeration

namespace LeetU.Services.Tests;

public class StudentServiceTests : IClassFixture<InMemoryDbContext>
{
    private readonly InMemoryDbContext _context;

    public StudentServiceTests(InMemoryDbContext context)
    {
        _context = context;
    }

    [Fact]
    public void ShouldGetStudents()
    {
        //Arrange
        _context.Reset();
        var sut = new StudentService(new StudentRepository(_context.StudentContext),
            new StudentCourseRepository(_context.StudentContext),
            new CourseRepository(_context.StudentContext));

        //Act
        var students = sut.GetStudents().ToArray();

        //Assert
        Assert.Equal(_context.StudentContext.Students.Count(), students.Count());

        for (var i = 1; i < _context.StudentContext.Students.Count() + 1; i++) 
            Assert.Equal(i, students[i - 1].Id);
    }

    [Fact]
    public void ShouldGetStudentsWithCourses()
    {
        //Arrange
        _context.Reset();
        var sut = new StudentService(new StudentRepository(_context.StudentContext),
            new StudentCourseRepository(_context.StudentContext), 
            new CourseRepository(_context.StudentContext));

        //Act
        var studentsWithCourses = sut.GetStudentsWithCourses();
        var studentCourseData = _context.StudentContext.StudentCourses.ToList();

        //Assert
        foreach (var student in studentsWithCourses)
        {
            var courseData = studentCourseData.Where(x => x.StudentId == student.Id).ToList();
            Assert.True(courseData.All(x => x.StudentId == student.Id));
        }
    }
}