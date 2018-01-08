using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Model
{
    public class Student : User
    {
        public string College { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }

        public Student(SqlConnection connection) : base(connection)
        {
        }
    }
}
