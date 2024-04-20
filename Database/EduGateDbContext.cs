using Database.Models;
using Microsoft.EntityFrameworkCore;


// package manager console commands:
// SET DATABASE AS STARTUP PROJECT !!!
// Add-Migration YourMigrationName
// Update-Database

// if ERROR -> dotnet add package Microsoft.EntityFrameworkCore.Tools 

// delete database -> 

namespace Database
{
    public class EduGateDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudyProgram> StudyPrograms { get; set; }
        public DbSet<Application> Applications { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string relativePath = @"..\Database\EduGate.db";
            //string currentDirectory = Directory.GetCurrentDirectory();
            //string absolutePath = Path.GetFullPath(Path.Combine(currentDirectory, relativePath));
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\vojta\source\repos\EduGate\Database\EduGate.db");
        }
    }
}
