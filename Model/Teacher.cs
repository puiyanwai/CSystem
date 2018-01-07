using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Common;

namespace Model
{
    public class Teacher
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public SexType Sex { set; get; }
        public string Brand { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }
    }
}
