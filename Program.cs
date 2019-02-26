using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lab_7
{
    class Program
    {
        //  The List() method lists our the course and the students in the course in a neat format.
        static void List()
        {
            using (var db = new Lab_7Context())
            {
                var allStuff = db.Courses.Include(c => c.StudentCourses).ThenInclude(s => s.Student);

                foreach (var course in allStuff)
                {
                    Console.WriteLine($"{course.Name}");
                    foreach (var student in course.StudentCourses)
                    {
                        Console.WriteLine($"\t{student.Student.FirstName} {student.Student.LastName}, {student.GPA:N}");
                    }
                    Console.WriteLine();
                }
            }
        }
        static void Main(string[] args)
        {
            using (var db = new Lab_7Context())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                //  The courses in the database.
                List<Course> courses = new List<Course>()
                {
                    new Course {Name = "Programming Business Applications"},
                    new Course {Name = "Software Systems Development"}
                };

                //  The students in the database.
                List<Student> students = new List<Student>()
                {
                    new Student {FirstName = "Ariana", LastName = "Rangel"},
                    new Student {FirstName = "Alex", LastName = "Tiroff"},
                    new Student {FirstName = "Becca", LastName = "Gonzales"},
                    new Student {FirstName = "Tomas", LastName = "Sanchez"}
                };

                //  Connects the students with which course they are in as well as their GPA.
                List<StudentCourse> joinedTable = new List<StudentCourse>()
                {
                    new StudentCourse {Student = students[0], Course = courses[0], GPA = 3.5},
                    new StudentCourse {Student = students[1], Course = courses[0], GPA = 4.0},
                    new StudentCourse {Student = students[2], Course = courses[0], GPA = 3.6},
                    new StudentCourse {Student = students[1], Course = courses[1], GPA = 3.7},
                    new StudentCourse {Student = students[3], Course = courses[1], GPA = 3.0},
                };

                db.AddRange(students);
                db.AddRange(courses);
                db.AddRange(joinedTable);
                db.SaveChanges();
            }
            List();

            //  Removes a student (Tomas Sanchez) from one of the courses (Software Systems Development). 
            //  The student still exists but is no longer enrolled in the course.
            using (var db = new Lab_7Context())
            {
                int studentID = 4;
                int courseID = 2;

                StudentCourse studentRemove = db.StudentCourses.Find(studentID, courseID);
                Student s = db.Students.Find(studentID);
                Course c = db.Courses.Find(courseID);
                db.Remove(studentRemove);
                db.SaveChanges();
            }

            //  Adds a new transfer student (Julian Garcia) and enrolls that student in one course (Programming Business Applications).
            using (var db = new Lab_7Context())
            {
                Student transferStudent = new Student();
                transferStudent.FirstName = "Julian";
                transferStudent.LastName = "Garcia";
                StudentCourse transferCourse = new StudentCourse {Student = transferStudent, CourseID = 1, GPA = 2.7};
                db.Add(transferStudent);
                db.Add(transferCourse);
                db.SaveChanges();
            }
            List();
        }
    }
}
