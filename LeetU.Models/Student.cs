using LeetU.Models.Interfaces;

namespace LeetU.Models;

public class Student : IStudent
{
    public long Id { get; set; }
    public string? Name {get; set;}
    public string? Surname {get;set;}
    public DateTime DateOfBirth {get; set; }
    public Sex Sex { get; set;}
    public string? TelephoneNumber {get;set;}
    public Address? Address {get;set;}
}