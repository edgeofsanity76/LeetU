using LeetU.Data.Context;
using LeetU.Data.Entites;
using LeetU.Data.Interfaces;

namespace LeetU.Data.Repositories;

public class StudentRepository : RepositoryBase<Student>, IStudentRepository
{
    public StudentRepository(StudentContext context) : base(context)
    {
    }
}