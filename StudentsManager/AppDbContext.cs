using Microsoft.EntityFrameworkCore;
using StudentsManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager
{
    public class AppDbContext : DbContext
    {
        private const string ConnectionString =
            "Data Source=C:\\Users\\79053\\Desktop\\StudentsManager — UIX\\StudentsManager\\university.db";
  
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Visit> Visits => Set<Visit>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Group> Groups => Set<Group>();
    }
}
