using LeetU.Data.Interfaces;
using LeetU.Models;
using LeetU.Services.Exceptions;
using LeetU.Services.Interfaces;
using LeetU.Services.Mappers;

namespace LeetU.Services;

/// <summary>
/// The student service. Contains the BUSINESS LOGIC for anything to do with courses. This includes marshalling the data access for controllers to the data layer.
/// We use mappers to map Entities to Models and vice versa. We do not send entities over the wire, we always transform them to models
/// </summary>
public class StudentService : IStudentService
{
    private readonly IStudentRepositoryCrud _studentRepositoryCrud;
    private readonly IStudentCourseRepositoryCrud _studentCourseRepositoryCrud;
    private readonly ICourseRepository _courseRepository;

    public StudentService(IStudentRepositoryCrud studentRepositoryCrud, 
                          IStudentCourseRepositoryCrud studentCourseRepositoryCrud,
                          ICourseRepository courseRepository)
    {
        _studentRepositoryCrud = studentRepositoryCrud;
        _studentCourseRepositoryCrud = studentCourseRepositoryCrud;
        _courseRepository = courseRepository;
    }

    public bool HasCourse(long studentId, long courseId)
    {
        return _studentCourseRepositoryCrud.HasCourse(studentId, courseId);
    }

    public async Task<int> SetStudentAsync(Student student)
    {
        var entity = ModelToEntity.CreateEntityFromStudent(student);
        await _studentRepositoryCrud.InsertAsync(entity);
        var rowsAffected = await _studentRepositoryCrud.SaveChangesAsync();
        return rowsAffected;
    }

    public async Task<int> UpdateStudentAsync(Student student)
    {
        var entity = ModelToEntity.UpdateEntityFromStudent(_studentRepositoryCrud.Get(s => s.Id == student.Id).FirstOrDefault()!, student);
        _studentRepositoryCrud.Update(entity);
        var rowsAffected = await _studentRepositoryCrud.SaveChangesAsync();
        return rowsAffected;
    }

    public IEnumerable<StudentWithCourses> GetStudentsWithCourses(params long[] studentIds)
    {
        var entities = _studentCourseRepositoryCrud.Get(s => studentIds.Any(id => s.StudentId == id) || studentIds.Length == 0, null, "Student", "Course", "Student.Address");

        var groupedCourses = entities.GroupBy(g => g.Student);

        foreach (var group in groupedCourses)
        {
            var student = group.Key;
            var studentWithCourses = EntityToModel.CreateStudentFromEntity<StudentWithCourses>(student) ;
            var courses = group.Select(x => x.Course);

            studentWithCourses.Courses = EntityToModel.CreateCoursesFromEntities(courses);

            yield return studentWithCourses;
        }
    }

    public async Task<int> SetStudentCourseAsync(long studentId, long courseId)
    {
        if (_studentCourseRepositoryCrud.HasCourse(studentId, courseId))
            throw new CourseAlreadyAssignedException(studentId, courseId);
        
        var courseEntity = await _courseRepository.GetAsync(courseId);

        if (courseEntity == null)
            throw new EntityNotFoundException($"The course with id {courseId} was not found");

        var studentEntity = _studentCourseRepositoryCrud.Get(s => s.StudentId == studentId).FirstOrDefault();

        if (studentEntity == null)
            throw new EntityNotFoundException($"The student with id {studentId} was not found");

        studentEntity.Course = courseEntity;

        _studentCourseRepositoryCrud.Update(studentEntity);

        var rowsAffected = await _courseRepository.SaveChangesAsync();
        return rowsAffected;
    }

    public IEnumerable<Student> GetStudents(params long[] studentIds)
    {
        var entities = _studentRepositoryCrud.Get(s => studentIds.Any(id => s.Id == id) || studentIds.Length == 0, null, "Address");

        foreach (var entity in entities)
            yield return EntityToModel.CreateStudentFromEntity<Student>(entity);
    }
}