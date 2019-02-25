using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Lab_7
{
    public class Lab_7Context: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(e => new {e.StudentID, e.CourseID});
        }
        public DbSet<Student> Students {get; set;}
        public DbSet<Course> Courses {get; set;}
        public DbSet<StudentCourse> StudentCourses {get; set;}
    }
    public class Student
    {
        public int StudentID {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public List<StudentCourse> StudentCourses {get; set;} // Navigation Property. Student can have MANY StudentCourses.
    }

    public class Course
    {
        public int CourseID {get; set;}
        public string Name {get; set;}
        public List<StudentCourse> StudentCourses {get; set;} // Navigation Property. Courses can have Many StudentCourses.
    }

    public class StudentCourse
    {
        public int StudentID {get; set;} // Composite Primary Key, Foreign Key 1.
        public int CourseID {get; set;} // Composite Primary Key, Foreign Key 2.
        public Student Student {get; set;} // Navigation Property. One student per StudentCourses.
        public Course Course {get; set;} // Navigation Property. One course per StudentCourses.
        public double GPA {get; set;}
    }
}