﻿using Database.Models;
using Microsoft.EntityFrameworkCore;


// package manager console commands:
// SET DATABASE AS STARTUP PROJECT !!!
// Add-Migration YourMigrationName
// Update-Database

// if ERROR -> dotnet add package Microsoft.EntityFrameworkCore.Tools 

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
