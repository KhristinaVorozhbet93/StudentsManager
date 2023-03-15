using Microsoft.EntityFrameworkCore;
using StudentsManager.Entities;
using System;
using System.IO;


namespace StudentsManager
{
    public class AppDbContext : DbContext
    {
        private const string ConnectionString =
            "Data Source=C:\\Users\\79053\\Desktop\\StudentsManager-UIX\\StudentsManager\\university.db";
        private string _logFileName = "C:\\Users\\79053\\Desktop\\StudentsManager-UIX2\\StudentsManager\\bin\\Debug\\net7.0-windows\\logFile.txt";

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(ConnectionString)
                .LogTo(line =>
                {
                    File.AppendAllText(
                        _logFileName, line + Environment.NewLine);
                })
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .OwnsOne(s => s.Email, builder =>
                    builder.Property(it => it.Value).HasColumnName("Email"))
                .OwnsOne(s => s.Passport, builder =>
                    builder.Property(it => it.Value).HasColumnName("Passport"));
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Visit> Visits => Set<Visit>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Group> Groups => Set<Group>();
    }
}
