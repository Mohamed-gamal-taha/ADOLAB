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
    public class CourseRepository
    {
        private readonly string connectionString;
        public CourseRepository(string conn) => connectionString = conn;
        internal void Add(Course course)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Course(Title, Credits) VALUES(@Title, @Credits)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", course.Title);
                cmd.Parameters.AddWithValue("@Credits", course.Credits);
                cmd.ExecuteNonQuery();
            }
        }
        internal List<Course> GetAll()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT CourseId, Title, Credits FROM Course";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        CourseId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Credits = reader.GetInt32(2)
                    });
                }
            }
            return courses;
        }
    }
}
