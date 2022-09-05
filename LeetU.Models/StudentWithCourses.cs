using LeetU.Models.Interfaces;

namespace LeetU.Models
{
    public class StudentWithCourses : Student, IStudentWithCourses
    {
        public IEnumerable<Course> Courses { get;set; }
    }
}
