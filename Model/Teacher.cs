using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Model
{
    public class Teacher : User
    {
        public string Brand { private set; get; }

        public Teacher(SqlConnection conn,
            int id,
            string name,
            SexType sex,
            string brand,
            string phone) : base(conn, id, name, sex, phone)
        {
            Brand = brand;
        }
    }
}
