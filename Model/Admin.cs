using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Model
{
    public class Admin : User
    {
        public Admin(SqlConnection conn,
            int id,
            string name = "",
            SexType sex = SexType.Unknown,
            string phone = "") : base(conn, id, name, sex, phone)
        {
        }
    }
}
