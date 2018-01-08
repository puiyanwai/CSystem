using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDAL;
using Model;

namespace SystemBLL
{
    public class LoginManager
    {
        public static Student StudentLogin(string name, string password)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource);

            string cmd = "select * from Student where \"id\"=@u and \"pwd\"=@p ";
            var result = SqlHelper.ExecuteScalar(
                conn, cmd,
                parameters: new SqlParameter[] {
                    new SqlParameter("@u", SqlDbType.NVarChar, 50) { Value = name },
                    new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = password }
                });
            if (result != null)
            {
                return new Student(conn) { Name = name, Id = name };
            }
            else
            {
                conn.Close();
                return null;
            }
        }

        public static Teacher TeacherLogin(string name, string password)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource);

            string cmd = "select * from Teacher where \"id\"=@u and \"pwd\"=@p ";
            var result = SqlHelper.ExecuteScalar(
                conn, cmd,
                parameters: new SqlParameter[] {
                    new SqlParameter("@u", SqlDbType.NVarChar, 50) { Value = name },
                    new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = password }
                });
            if (result != null)
            {
                return new Teacher(conn) { Name = name, Id = name };
            }
            else
            {
                conn.Close();
                return null;
            }
        }
    }
}
