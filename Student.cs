using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ADOlab.Entities
{
    internal class Student
    {
            public int StudentId { get; set; } 
            public string Name { get; set; }
            public int Level { get; set; }
        
    }
}
