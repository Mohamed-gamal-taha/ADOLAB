using ADOlab.Entities;
using ADOlab.Reposatories;
using Microsoft.Data.SqlClient;

namespace ADOlab
{
    internal class Program
    {
        static void Main(string[] args)
        {   
                string connectionString = "Server=.\\SQLEXPRESS;Database=UniversityDB;Trusted_Connection=True;TrustServerCertificate=True;";
                var studentRepo = new StudentRepository(connectionString);
                var courseRepo = new CourseRepository(connectionString);
                var enrollmentRepo = new EnrollmentRepository(connectionString);

                bool running = true;
                while (running)
                {
                    Console.Clear();
                    Console.WriteLine("==== University Enrollment System ====");
                    Console.WriteLine("1. Add Student");
                    Console.WriteLine("2. Add Course");
                    Console.WriteLine("3. Enroll Student in Course");
                    Console.WriteLine("4. Show All Students");
                    Console.WriteLine("5. Show All Courses");
                    Console.WriteLine("6. Show All Enrollments (with JOIN)");
                    Console.WriteLine("7. Show Honor Roll Students");
                    Console.WriteLine("0. Exit");
                    Console.Write("Choose: ");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.Write("Student Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Level: ");
                            int level = int.Parse(Console.ReadLine());
                            studentRepo.Add(new Student { Name = name, Level = level });
                            break;

                        case "2":
                            Console.Write("Course Title: ");
                            string title = Console.ReadLine();
                            Console.Write("Credits: ");
                            int credits = int.Parse(Console.ReadLine());
                            courseRepo.Add(new Course { Title = title, Credits = credits });
                            break;

                        case "3":
                            Console.Write("StudentId: ");
                            int sid = int.Parse(Console.ReadLine());
                            Console.Write("CourseId: ");
                            int cid = int.Parse(Console.ReadLine());
                            Console.Write("Semester: ");
                            string sem = Console.ReadLine();
                            Console.Write("Grade (or leave empty): ");
                            string gradeInput = Console.ReadLine();
                            decimal grade =  decimal.Parse(gradeInput);
                            enrollmentRepo.Add(new Enrollment { StudentId = sid, CourseId = cid, Semester = sem, Grade = grade });
                            break;

                        case "4":
                            foreach (var s in studentRepo.GetAll())
                                Console.WriteLine($"{s.StudentId} - {s.Name} (Level {s.Level})");
                            Console.ReadKey();
                            break;

                        case "5":
                            foreach (var c in courseRepo.GetAll())
                                Console.WriteLine($"{c.CourseId} - {c.Title} ({c.Credits} credits)");
                            Console.ReadKey();
                            break;

                        case "6":
                            foreach (var e in enrollmentRepo.GetAll())
                                Console.WriteLine($"{e.StudentName} - {e.CourseTitle} ({e.Semester}) Grade: {e.Grade}");
                            Console.ReadKey();
                            break;

                        case "7":
                            Console.WriteLine("=== Honor Roll Students (GPA >= 3.5) ===");
                            var honorRoll = enrollmentRepo.GetHonorRoll();
                            if (honorRoll.Count == 0)
                                Console.WriteLine("No students qualified for the honor roll.");
                            else
                                foreach (var h in honorRoll)
                                    Console.WriteLine($"{h.StudentName} - GPA: {h.GPA:F2}");
                            Console.ReadKey();
                            break;

                        case "0":
                            running = false;
                            break;
                    }
                }
            }
        }

    }
  
    

