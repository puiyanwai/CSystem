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
        public static SqlConnection OpenDatabase(string user, string pass, string source, string dbname)
        {
            try
            {
                SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder()
                {
                    DataSource = source,
                    InitialCatalog = dbname,
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
        /// <param name="parameters">参数列表，默认为null</param>
        /// <param name="type">命令类型，默认为Text</param>
        /// <returns>执行结果</returns>
        public static DataTable ExecuteDataTable(
            SqlConnection connection,
            string command,
            SqlParameter[] parameters = null,
            CommandType type = CommandType.Text)
        {
            try
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
            catch (Exception e)
            {
                // TODO Log exception
                return null;
            }
        }

        /// <summary>
        /// 对一个链接执行一次命令
        /// </summary>
        /// <param name="connection">SQL连接</param>
        /// <param name="command">命令</param>
        /// <param name="parameters">参数列表，默认为null</param>
        /// <param name="type">命令类型，默认为Text</param>
        /// <returns>执行结果的第一行第一列</returns>
        public static Object ExecuteScalar(
            SqlConnection connection,
            string command,
            SqlParameter[] parameters = null,
            CommandType type = CommandType.Text)
        {
            try
            {
                using (var cmd = new SqlCommand(command, connection) { CommandType = type })
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                // TODO Log exception
                return null;
            }
        }

        /// <summary>
        /// 对一个链接执行一次非查询指令
        /// </summary>
        /// <param name="connection">SQL连接</param>
        /// <param name="command">命令</param>
        /// <param name="parameters">参数列表，默认为null</param>
        /// <param name="type">命令类型，默认为Text</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(
            SqlConnection connection,
            string command,
            SqlParameter[] parameters = null,
            CommandType type = CommandType.Text)
        {
            try
            {
                using (var cmd = new SqlCommand(command, connection) { CommandType = type })
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                // TODO Log exception
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">SQL连接</param>
        /// <param name="command">命令</param>
        /// <param name="behavior"></param>
        /// <param name="parameters">参数列表，默认为null</param>
        /// <param name="type">命令类型，默认为Text</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(
            SqlConnection connection,
            string command,
            CommandBehavior behavior = CommandBehavior.CloseConnection,
            SqlParameter[] parameters = null,
            CommandType type = CommandType.Text)
        {
            try
            {
                using (var cmd = new SqlCommand(command, connection) { CommandType = type })
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteReader(behavior);
                }
            }
            catch (Exception e)
            {
                // TODO Log exception
                return null;
            }
        }

    }
}

