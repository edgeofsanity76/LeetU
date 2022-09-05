namespace LeetU.Data.Entites
{
    public partial class StudentCourse
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long CourseId { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
