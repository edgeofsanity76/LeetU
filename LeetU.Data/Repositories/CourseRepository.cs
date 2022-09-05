using LeetU.Data.Context;
using LeetU.Data.Entites;
using LeetU.Data.Interfaces;

namespace LeetU.Data.Repositories
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(StudentContext context) : base(context)
        {
        }
    }
}
