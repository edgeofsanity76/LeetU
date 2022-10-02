namespace LeetU.Models.Interfaces
{
    internal interface IStudentWithCourses : IStudent
    {
        IEnumerable<Course>? Courses { get;set; }
    }
}
