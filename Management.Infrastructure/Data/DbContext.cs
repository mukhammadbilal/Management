using Management.Domain.Models;

namespace Management.Infrastructure.Data;

public class DbContext
{
    public DbContext()
    {
        this.Students = new Student[12];
    }

    public Student[] Students { get; set; }
    public int StudentCount { get; set; } = 0;
    public List<Teacher> Teachers { get; set; } = new ()
    {
        new Teacher { Id = 1, FirstName = "Elbek", LastName = "Normurodov", Username = "admin", Password = "admin123" },
        new Teacher { Id = 2, FirstName = "Nodirxon", LastName = "Abdumurotov", Username = "user", Password = "user123" }
    };
}
