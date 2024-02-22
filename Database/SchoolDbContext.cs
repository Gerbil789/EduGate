using Database.Models;
using Microsoft.EntityFrameworkCore;

// package manager console commands:
// dotnet add package Microsoft.EntityFrameworkCore.Tools
// Add-Migration YourMigrationName
// Update-Database

namespace Database
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudyProgram> StudyPrograms { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=school.db");
        }
    }
}
