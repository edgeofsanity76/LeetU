using LeetU.Data.Context;
using LeetU.Data.Entites;
using LeetU.Data.Interfaces;

namespace LeetU.Data.Repositories;

public class StudentCourseRepository : RepositoryBase<StudentCourse>, IStudentCourseRepository
{
    public StudentCourseRepository(StudentContext context) : base(context)
    {
    }
}