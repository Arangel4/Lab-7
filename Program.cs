using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lab_7
{
    class Program
    {
        using (var db = new Lab_7Context())
        {
            var allStuff = db.courses.Include(c => c.StudentCourses).ThenInclude(s => s.Student);

            foreach (var course in allStuff)
            {
                Console.WriteLine($"{course.Name} -");
                foreach (var student in course.StudentCourses)
                {
                    Console.WriteLine($"\t{student.Student.FirstName} {student.Student.LastName} {student.GPA}");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            using (var db = new Lab_7Context())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                List<Course> courses = new List<Course>()
                {
                    new Course {Name = "Programming Business Applications"},
                    new Course {Name = "Software Systems Development"}
                };

                List<Student> students = new List<Student>()
                {
                    new Student {FirstName = "Ariana", LastName = "Rangel"},
                    new Student {FirstName = "Alex", LastName = "Tiroff"},
                    new Student {FirstName = "Becca", LastName = "Gonzales"},
                    new Student {FirstName = "Tomas", LastName = "Sanchez"}
                };

                List<StudentCourse> joinedTable = new List<StudentCourse>()
                {
                    new StudentCourse {Student = students[0], Course = courses[0], GPA = 3.5},
                    new StudentCourse {Student = students[1], Course = courses[0], GPA = 3.7},
                    new StudentCourse {Student = students[2], Course = courses[0], GPA = 3.6},
                    new StudentCourse {Student = students[3], Course = courses[0], GPA = 3.0},
                    new StudentCourse {Student = students[0], Course = courses[1], GPA = 3.5},
                    new StudentCourse {Student = students[1], Course = courses[1], GPA = 3.7},
                    new StudentCourse {Student = students[2], Course = courses[1], GPA = 3.6},
                    new StudentCourse {Student = students[3], Course = courses[1], GPA = 3.0},
                };

                db.AddRange(students);
                db.AddRange(courses);
                db.AddRange(joinedTable);
                db.SaveChanges();
            }
            List();
        }
    }
}
