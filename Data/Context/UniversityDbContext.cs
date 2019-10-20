using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Data.Context
{
    public class UniversityDbContext: DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BankDbContext, Migrations.Configuration>());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Mark> Marks { get; set; }
    }
}
