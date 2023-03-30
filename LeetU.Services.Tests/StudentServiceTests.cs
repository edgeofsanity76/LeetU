using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;
using LeetU.Data.Repositories;
using LeetU.Data.Tests.DataContext;
using LeetU.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Xunit;
// ReSharper disable PossibleMultipleEnumeration

namespace LeetU.Services.Tests;

/// <summary>
/// StudentService tests. Here we are using the InMemoryDbContext we set up in the Data.Tests. It's completely reusable and provides a nice way of providing test data to our tests.
/// </summary>
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

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void ShouldGetStudentsById(int studentId)
    {
        //Arrange
        _context.Reset();
        var sut = new StudentService(new StudentRepository(_context.StudentContext),
            new StudentCourseRepository(_context.StudentContext),
            new CourseRepository(_context.StudentContext));

        //Act
        var student = sut.GetStudents(studentId).FirstOrDefault();

        //Assert
        Assert.Equal(studentId, student!.Id);
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

    [Fact]
    public async Task ShouldSetStudentCourse()
    {
        //Arrange
        _context.Reset();
        var sut = new StudentService(new StudentRepository(_context.StudentContext),
            new StudentCourseRepository(_context.StudentContext),
            new CourseRepository(_context.StudentContext));

        //Act
        await sut.SetStudentCourseAsync(1, 5);
        var student = _context.StudentContext.Students.Include(s => s.StudentCourses).FirstOrDefault(s => s.Id == 1);

        //Assert
        Assert.True(student!.StudentCourses.Count(s => s.CourseId == 5) == 1);
    }

    [Fact]
    public async Task ShouldNotSetCourseIfCourseAlreadyAssgined()
    {
        _context.Reset();
        var sut = new StudentService(new StudentRepository(_context.StudentContext),
            new StudentCourseRepository(_context.StudentContext),
            new CourseRepository(_context.StudentContext));

        //Act
        await sut.SetStudentCourseAsync(1, 5);

        //Assert
        await Assert.ThrowsAsync<CourseAlreadyAssignedException>(() => sut.SetStudentCourseAsync(1, 5));
    }
}