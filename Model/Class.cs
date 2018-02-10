using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Model
{
    public class Class
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int TeacherId { set; get; }
        public string TeacherName { set; get; }
        public string Category { set; get; }
        public string Time { set; get; }
        public string Place { set; get; }
        public int Capacity { set; get; }
        public double UsualProportion { set; get; }
    }


}
