using System;
using System.Linq;
using System.Threading.Tasks;
using LeetU.Data.Repositories;
using LeetU.Data.Tests.DataContext;
using LeetU.Models;
using Xunit;

namespace LeetU.Services.Tests
{
    public class CourseServiceTests : IClassFixture<InMemoryDbContext>
    {
        private readonly InMemoryDbContext _context;

        public CourseServiceTests(InMemoryDbContext context)
        {
            _context = context;
        }

        [Fact]
        public void ShouldGetCourses()
        {
            //Arrange
            _context.Reset();
            var sut = new CourseService(new CourseRepository(_context.StudentContext));

            //Act
            var courses = sut.GetCourses();

            //Act
            Assert.Equal(_context.StudentContext.Courses.Count(), courses.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ShouldGetCoursesById(int courseId)
        {
            //Arrange
            _context.Reset();
            var sut = new CourseService(new CourseRepository(_context.StudentContext));

            //Act
            var course = sut.GetCourses(courseId).FirstOrDefault();

            //Act
            Assert.Equal(courseId, course!.Id);
        }

        [Fact]
        public async Task ShouldSetCourse()
        {
            //Arrange
            _context.Reset();
            var sut = new CourseService(new CourseRepository(_context.StudentContext));

            //Act
            var newCourse = new Course()
            {
                Description = "NewCourseDescription",
                Id = 11,
                Name = "NewCourse",
                StartDate = DateTime.Parse("01/01/2021")
            };

            await sut.SetCourseAsync(newCourse);

            var course = sut.GetCourses(11).FirstOrDefault();

            //Assert
            Assert.Equal(11, course!.Id);
            Assert.Equal("NewCourse", course!.Name);
            Assert.Equal("NewCourseDescription", course!.Description);
            Assert.Equal("01/01/2021", course.StartDate.ToShortDateString());
        }
    }
}
