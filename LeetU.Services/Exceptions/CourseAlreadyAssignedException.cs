namespace LeetU.Services.Exceptions;

public class CourseAlreadyAssignedException : Exception
{
    public CourseAlreadyAssignedException(long studentId, long courseId) : base($"A course with id {courseId} has already been assigned to student id {studentId}")
    {
    }
}