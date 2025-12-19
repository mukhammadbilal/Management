using Management.Application.Services;
using Management.Infrastructure.Data;

var dbContext = new DbContext();
var studentService = new StudentService(dbContext);

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Welcome to Student Management System");
Console.WriteLine("------------------------------------");
Console.WriteLine("1. Login");
Console.WriteLine("2. Exit");
Console.ResetColor();

Console.Write("\nSelect an option: ");
string option = Console.ReadLine();


if (option == "1")
{
    var loginService = new LoginService(dbContext, studentService);
    loginService.Login();
}
else
{
    Console.WriteLine("Exiting...");
}


