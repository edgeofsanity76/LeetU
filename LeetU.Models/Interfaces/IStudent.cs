namespace LeetU.Models.Interfaces;

public interface IStudent
{
    long Id { get; set; }
    string? FirstName { get; set; }
    string? Surname { get; set; }
    DateTime DateOfBirth { get; set; }
    Sex Sex { get; set; }
    string? TelephoneNumber { get; set; }
    Address? Address { get; set; }
}