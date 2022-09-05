namespace LeetU.Data.Entites
{
    public partial class Course
    {
        public Course()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? StartDate { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
