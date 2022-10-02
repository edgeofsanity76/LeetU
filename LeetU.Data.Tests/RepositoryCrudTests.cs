using System.Linq;
using System.Threading.Tasks;
using LeetU.Data.Entities;
using LeetU.Data.Tests.DataContext;
using Xunit;
// ReSharper disable PossibleMultipleEnumeration

namespace LeetU.Data.Tests;

/// <summary>
/// Basic tests for CRUD operations
/// </summary>
public class RepositoryCrudTests : IClassFixture<InMemoryDbContext>
{
    private readonly InMemoryDbContext _context;

    public RepositoryCrudTests(InMemoryDbContext context) 
    {
        _context = context;
    }

    [Fact]
    public void ShouldGetEntityWithSpecifiedId()
    {
        //Arrange
        _context.Reset();
        var sut =  new TestRepository<Student>(_context.StudentContext);

        //Act
        var entities = sut.Get(x => x.Id == 1);

        //Assert
        Assert.Collection(entities, student => student.Id = 1);
    }

    [Fact]
    public void ShouldGetEntityWithSpecifiedIdAndIncludeAddressEntity()
    {
        //Arrange
        _context.Reset();
        var sut = new TestRepository<Student>(_context.StudentContext);

        //Act
        var entities = sut.Get(x => x.Id == 1, null, "Address");

        //Assert
        Assert.Collection(entities, student => student.Id = 1);
        Assert.NotNull(entities.First().Address);
        Assert.Equal(1, entities.First().Address.Id);
    }

    [Fact]
    public void ShouldGetAllStudentEntities()
    {
        //Arrange
        _context.Reset();
        var sut = new TestRepository<Student>(_context.StudentContext);

        //Act
        var entities = sut.Get();

        //Assert
        Assert.Equal(10, entities.Count());
    }

    [Fact]
    public void ShouldGetAllStudentEntitiesAndOrderByDescending()
    {
        //Arrange
        _context.Reset();
        var sut = new TestRepository<Student>(_context.StudentContext);

        //Act
        var entities = sut.Get(null, students => students.OrderByDescending(x => x.Id));

        //Assert
        Assert.Equal(10, entities.Count());
        Assert.Equal(1, entities.Last().Id);
        Assert.Equal(10, entities.First().Id);
    }

    [Fact]
    public async Task ShouldAddNewEntity()
    {
        //Arrange
        _context.Reset();
        var sut = new TestRepository<Student>(_context.StudentContext);

        //Act
        var newStudent = new Student()
        {
            AddressId = 1,
            Name = "New",
            DateOfBirth = "01/01/2021",
            Sex = 1,
            Surname = "NewSurname"
        };

        await sut.InsertAsync(newStudent);
        await sut.SaveChangesAsync();

        var entities = sut.Get(x => x.Id == 11);

        //Assert
        Assert.Collection(entities, student => student.Id = 11);
        Assert.Equal("New", entities.First().Name);
        Assert.Equal(1, entities.First().Sex);
        Assert.Equal("NewSurname", entities.First().Surname);
        Assert.Equal("01/01/2021", entities.First().DateOfBirth);
    }

    [Fact]
    public async Task ShouldDeleteEntity()
    {
        //Arrange
        _context.Reset();
        var sut = new TestRepository<Student>(_context.StudentContext);

        //Act
        var newStudent = new Student()
        {
            AddressId = 1,
            Name = "New",
            DateOfBirth = "01/01/2021",
            Sex = 1,
            Surname = "NewSurname"
        };

        await sut.InsertAsync(newStudent);
        await sut.SaveChangesAsync();

        var entities = sut.Get(x => x.Id == 11);
        var student = entities.First();

        sut.Delete(student);
        await sut.SaveChangesAsync();

        entities = sut.Get();

        //Assert
        Assert.Equal(10, entities.Count());
    }

    [Fact]
    public async Task ShouldUpdateEntity()
    {
        //Arrange
        _context.Reset();
        var sut = new TestRepository<Student>(_context.StudentContext);

        //Act
        var entities = sut.Get(x => x.Id == 1);
        var student = entities.First();

        student.Name = "UpdatedName";

        sut.Update(student);

        await sut.SaveChangesAsync();

        entities = sut.Get(x => x.Id == 1);

        //Assert
        Assert.Equal("UpdatedName", entities.First().Name);
    }
}