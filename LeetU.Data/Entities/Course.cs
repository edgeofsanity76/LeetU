﻿namespace LeetU.Data.Entities
{
    /// <summary>
    /// An entity. Represents a row in the database. It can also represent a return from a stored procedure. There is no need to alter this as it is auto generated by the context builder
    /// </summary>
    public partial class Course
    {
        public Course()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        public long Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? StartDate { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
