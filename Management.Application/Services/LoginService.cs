using Management.Domain.Models;
using Management.Infrastructure.Data;

namespace Management.Application.Services;

public class LoginService
{
    private readonly DbContext _dbContext;
    private readonly StudentService _studentService;

    public LoginService(DbContext dbContext, StudentService studentService)
    {
        _dbContext = dbContext;
        _studentService = studentService;
    }

    int loginAttempts = 2;
    public void Login()
    {

        do
        {

            Console.WriteLine();
            Console.WriteLine("----------Login----------");

            Console.Write("Enter username: ");
            string userName = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var teacher = _dbContext.Teachers.FirstOrDefault(t => t.Username == userName && t.Password == password);

            if (teacher != null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Login successful! Welcome, {teacher.FirstName} {teacher.LastName}");
                Console.ResetColor();

                ShowAndSelectMenu();
            }

            if (loginAttempts == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nMaximum login attempts reached. Exiting application");
                Console.ResetColor();
                return;
            }

            else if (loginAttempts == 3)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Back to Login page... You have {loginAttempts} attempts");
                Console.ResetColor();
            }
            else if (loginAttempts == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting application...");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nInvalid username or password. {loginAttempts} attempts left");
                Console.ResetColor();
                //Console.ReadKey();
            }


        } while (loginAttempts-- > 0);

    }

    void ShowAndSelectMenu()
    {
        bool isLoggedIn = true;

        while (isLoggedIn)
        {
            Console.WriteLine();
            Console.WriteLine("Main Menu");
            Console.WriteLine("--------------------");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Students");
            Console.WriteLine("3. Group Size");
            Console.WriteLine("4. Log out");
            Console.WriteLine("5. Exit");
            Console.WriteLine("--------------------");

            Console.Write("\nSelect an option: ");
            string selectedOption = Console.ReadLine();

            switch (selectedOption)
            {
                case "1":
                    _studentService.AddStudent();
                    break;

                case "2":
                    var students = _studentService.GetAllStudents();

                    foreach (var student in students)
                    {
                        if (student != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{students.IndexOf(student) + 1}.\tID: {student.Id} \t{student.FirstName} {student.LastName}");
                            Console.ResetColor();
                        }
                    }
                    break;

                case "3":
                    _studentService.GetGroupSize();
                    break;

                case "4":
                    loginAttempts = 3;
                    isLoggedIn = false;
                    break;

                case "5":
                    loginAttempts = -1;
                    isLoggedIn = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
