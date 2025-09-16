using ADOlab.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ADOlab.Reposatories
{
    public class EnrollmentRepository
    {
        private readonly string connectionString;
        public EnrollmentRepository(string conn) => connectionString = conn;

        public void Add(Enrollment enrollment)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Enrollment(StudentId, CourseId, Semester, Grade) " +
                               "VALUES(@StudentId, @CourseId, @Semester, @Grade)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentId", enrollment.StudentId);
                cmd.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
                cmd.Parameters.AddWithValue("@Semester", enrollment.Semester);
                cmd.Parameters.AddWithValue("@Grade", (object)enrollment.Grade ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public List<(string StudentName, string CourseTitle, string Semester, decimal? Grade)> GetAll()
        {
            var enrollments = new List<(string, string, string, decimal?)>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT s.Name, c.Title, e.Semester, e.Grade
                                FROM Enrollment e JOIN Student s ON e.StudentId = s.StudentId
                                JOIN Course c ON e.CourseId = c.CourseId";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    enrollments.Add((reader.GetString(0), reader.GetString(1),
                                    reader.GetString(2),
                                    reader.IsDBNull(3) ? (decimal?)null : reader.GetDecimal(3)));
                }
            }
            return enrollments;
        }

          public List<(string StudentName, decimal GPA)> GetHonorRoll(decimal minGpa = 3.5m)
        {
            var honorRoll = new List<(string, decimal)>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT s.Name, AVG(e.Grade) AS GPA
                             FROM Enrollment e
                             INNER JOIN Student s ON e.StudentId = s.StudentId
                             WHERE e.Grade IS NOT NULL
                             GROUP BY s.Name
                             HAVING AVG(e.Grade) >= @MinGpa";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MinGpa", minGpa);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    honorRoll.Add((reader.GetString(0), reader.GetDecimal(1)));
                }
            }
            return honorRoll;
        }
    }
}