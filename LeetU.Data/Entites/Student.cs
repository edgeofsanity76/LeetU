namespace LeetU.Data.Entites
{
    public partial class Student
    {
        public Student()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public long Sex { get; set; }
        public long AddressId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
