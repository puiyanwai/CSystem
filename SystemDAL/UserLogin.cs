using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SystemDAL
{
    public class UserLogin
    {
        public UserLogin()
        { }

        #region Method

        //判断学生用户名，密码是否正确
        public bool StuLogin(string username, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [Student]");
            strSql.Append(" where id=@UserName and pwd=@UserPassword");
            SqlParameter[] parameters ={
                                           new SqlParameter ("@UserName",SqlDbType.VarChar,50),
                                           new SqlParameter ("@UserPassword",SqlDbType.VarChar ,50)
                                       };
            parameters[0].Value = username;
            parameters[1].Value = pwd;

            int n = Convert.ToInt32(SqlDbHelper.ExecuteScalar(strSql.ToString(), CommandType.Text, parameters));
            if (n == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断教师用户名，密码是否正确
        public bool TeaLogin(string username, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [Teacher]");
            strSql.Append(" where id=@UserName and pwd=@UserPassword");
            SqlParameter[] parameters ={
                                           new SqlParameter ("@UserName",SqlDbType.VarChar,50),
                                           new SqlParameter ("@UserPassword",SqlDbType.VarChar ,50)
                                       };
            parameters[0].Value = username;
            parameters[1].Value = pwd;

            int n = Convert.ToInt32(SqlDbHelper.ExecuteScalar(strSql.ToString(), CommandType.Text, parameters));
            if (n == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Method
    }
}
