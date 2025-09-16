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
    public class StudentRepository
    {
        private readonly string connectionString;
        public StudentRepository(string conn) => connectionString = conn;

        internal void Add(Student student)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Student(Name, Level) VALUES(@Name, @Level)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Level", student.Level);
                cmd.ExecuteNonQuery();
            }
        }

        internal List<Student> GetAll()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT StudentId, Name, Level FROM Student";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        StudentId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Level = reader.GetInt32(2)
                    });
                }
            }
            return students;
        }
    }
}

