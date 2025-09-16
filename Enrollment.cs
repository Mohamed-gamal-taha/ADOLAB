using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOlab.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Semester { get; set; }
        public decimal Grade { get; set; }
    }
}
