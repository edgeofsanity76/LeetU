using LeetU.Data.Interfaces;
using LeetU.Models;
using LeetU.Services.Exceptions;
using LeetU.Services.Interfaces;
using LeetU.Services.Mappers;

namespace LeetU.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IStudentCourseRepository _studentCourseRepository;
    private readonly ICourseRepository _courseRepository;

    public StudentService(IStudentRepository studentRepository, 
                          IStudentCourseRepository studentCourseRepository,
                          ICourseRepository courseRepository)
    {
        _studentRepository = studentRepository;
        _studentCourseRepository = studentCourseRepository;
        _courseRepository = courseRepository;
    }

    public IEnumerable<StudentWithCourses> GetStudentsWithCourses(params long[] studentIds)
    {
        var entities = _studentCourseRepository.Get(s => studentIds.Any(id => s.StudentId == id) || studentIds.Length == 0, null, "Student", "Course", "Student.Address");

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
        var courseEntity = await _courseRepository.GetAsync(courseId);

        if (courseEntity == null)
            throw new EntityNotFoundException($"The course with id {courseId} was not found");

        var studentEntity = _studentCourseRepository.Get(s => s.StudentId == studentId).FirstOrDefault();

        if (studentEntity == null)
            throw new EntityNotFoundException($"The student with id {studentId} was not found");

        studentEntity.Course = courseEntity;

        _studentCourseRepository.Update(studentEntity);

        var rowsAffected = await _courseRepository.SaveChanges();
        return rowsAffected;
    }

    public IEnumerable<Student> GetStudents(params long[] studentIds)
    {
        var entities = _studentRepository.Get(s => studentIds.Any(id => s.Id == id) || studentIds.Length == 0, null, "Address");

        foreach (var entity in entities)
            yield return EntityToModel.CreateStudentFromEntity<Student>(entity);
    }
}