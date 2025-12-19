using Management.Domain.Models;
using Management.Infrastructure.Data;

namespace Management.Application.Services;

public class StudentService
{
    private readonly DbContext _dbContext;

    public StudentService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddStudent()
    {
        if (_dbContext.StudentCount == 12)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The group is full. No more students can be added.");
            Console.ResetColor();
            return;
        }

        Console.Clear();
        Console.WriteLine("-----Add New Student-----");
        Console.Write("Enter student's first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter student's last name: ");
        string lastName = Console.ReadLine();

        var newStudent = new Student
        {
            Id = new Random().Next(1, 1000).ToString(),
            FirstName = firstName,
            LastName = lastName
        };

        _dbContext.Students[_dbContext.StudentCount] = newStudent;
        _dbContext.StudentCount++;

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nStudent added successfully!");
        Console.ResetColor();
    }

    public Student[] GetAllStudents()
    {
        Console.Clear();

        
        if (_dbContext.StudentCount == 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("No students found. Try to add new students");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--------Students List--------");
            Console.WriteLine();
            Console.WriteLine("№\tStudent ID\tFull Name");
            Console.ResetColor();
        }

        return _dbContext.Students;
    }

    public void GetGroupSize()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"Current group size: {_dbContext.StudentCount} students");
        Console.WriteLine($"Availabel spots: {_dbContext.Students.Length - _dbContext.StudentCount}");
        Console.ResetColor();
    }
}
