using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Grade
    {
        public string StudentId { set; get; }
        public string ClassId { set; get; }
        public double UsualGrade { set; get; }
        public double FinalGrade { set; get; }
        public double TotalGrade { set; get; }
    }
}
