using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SystemDAL
{
    public class SqlHelper
    {
        /// <summary>
        /// 打开一个数据库连接
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static SqlConnection OpenDatabase(string user, string pass, string source)
        {
            try
            {
                SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder()
                {
                    DataSource = source,
                    InitialCatalog = "TeachingManagementSystem",
                    UserID = user,
                    Password = pass,
                    //NetworkLibrary = "DBMSSOCN" // only for ip connection
                };
                var connection = new SqlConnection(connStr.ToString());
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                throw new Exception("Open database failed", e);
            }
        }

        /// <summary>
        /// 对一个连接执行一次命令
        /// </summary>
        /// <param name="connection">SQL连接</param>
        /// <param name="command">命令</param>
        /// <param name="type">命令类型，默认为Text</param>
        /// <param name="parameters">参数列表，默认为null</param>
        /// <returns>执行结果</returns>
        public static DataTable ExecuteDataTable(
            SqlConnection connection,
            string command,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            DataTable data = new DataTable();
            using (var cmd = new SqlCommand(command, connection) { CommandType = type })
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    adapter.Fill(data);
            }
            return data;
        }

        /// <summary>
        /// 对一个链接执行一次命令
        /// </summary>
        /// <param name="connection">SQL连接</param>
        /// <param name="command">命令</param>
        /// <param name="type">命令类型，默认为Text</param>
        /// <param name="parameters">参数列表，默认为null</param>
        /// <returns>执行结果的第一行第一列</returns>
        public static Object ExecuteScalar(
            SqlConnection connection,
            string command,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            using (var cmd = new SqlCommand(command, connection) { CommandType = type })
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 对一个链接执行一次非查询指令
        /// </summary>
        /// <param name="connection">SQL连接</param>
        /// <param name="command">命令</param>
        /// <param name="type">命令类型，默认为Text</param>
        /// <param name="parameters">参数列表，默认为null</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(
            SqlConnection connection,
            string command,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            using (var cmd = new SqlCommand(command, connection) { CommandType = type })
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">SQL连接</param>
        /// <param name="command">命令</param>
        /// <param name="behavior"></param>
        /// <param name="type">命令类型，默认为Text</param>
        /// <param name="parameters">参数列表，默认为null</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(
            SqlConnection connection,
            string command,
            CommandBehavior behavior = CommandBehavior.CloseConnection,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            using (var cmd = new SqlCommand(command, connection) { CommandType = type })
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteReader(behavior);
            }
        }

    }
}

