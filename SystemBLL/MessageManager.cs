using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SystemDAL;
using Model;
using Common;

namespace SystemBLL
{
    public class MessageManager
    {
        //显示全部教师信息
        public static DataTable DisplayTea()
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,name,sex,brand FROM Teacher ORDER BY id ASC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        //显示全部学生信息
        public static DataTable DisplayStu()
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,name,sex,college FROM Student ORDER BY id ASC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        //查询学生信息
        public static DataTable SelectedStu(int studentId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT name FROM Student WHERE id=@studentId ";
            SqlParameter[] parameters = {
                new SqlParameter("@studentId", SqlDbType.Int) { Value = studentId }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //查询教师信息
        public static DataTable SelectedTea(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT name FROM Teacher WHERE id=@teacherId ";
            SqlParameter[] parameters = {
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = teacherId }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //显示教师筛选
        public static DataTable SelectTea(string option)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,name,sex,brand FROM Teacher " + option + " ORDER BY id ASC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        //显示教师发布的通知
        public static DataTable DisplayNotice()
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,title,teacherId,time FROM Notice ORDER BY time DESC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        //获取选中通知的通知内容
        public static DataTable ReturnBody(int id)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT info FROM Notice WHERE id = @id";
            SqlParameter[] parameters = {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //教师新增通知
        public static bool AddNotice(Teacher user, string time, string title, string info)
        {
            string cmd = "INSERT INTO Notice(teacherId,time,title,info) VALUES (@teacherId, @time, @title, @info) ";
            SqlParameter[] parameters = {
                new SqlParameter("@time", SqlDbType.NVarChar, 50) { Value = time },
                new SqlParameter("@title", SqlDbType.NVarChar, 50) { Value = title },
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) },
                new SqlParameter("@info", SqlDbType.NVarChar, 100) { Value = info }
            };

            int result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, parameters, CommandType.Text);
            return result == 1;
        }

        //教师修改通知
        public static bool UpdateNotice(Teacher user,int id, string time, string title, string info)
        {
            string cmd = "UPDATE Notice SET time=@time,title=@title,info=@info WHERE teacherId=@teacherId AND id=@id ";
            SqlParameter[] parameters = {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@time", SqlDbType.NVarChar, 50) { Value = time },
                new SqlParameter("@title", SqlDbType.NVarChar, 50) { Value = title },
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) },
                new SqlParameter("@info", SqlDbType.NVarChar, 100) { Value = info }
            };

            int result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, parameters, CommandType.Text);
            return result == 1;
        }

        //教师删除通知
        public static bool DeleteNotice(Teacher user, int id)
        {
            string cmd = "DELETE FROM Notice WHERE id = @id AND teacherId = @teacherId ";
            SqlParameter[] parameters = {
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) },
                new SqlParameter("@id", SqlDbType.Int) { Value = id }
            };
            int result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, parameters, CommandType.Text);
            if (result > 0) return true;
            else return false;
        }

        //教师生成留言recevier是student    sender是教师 type=1
        public static bool AddMessage(Teacher user, int studentId, int type, string time, string info)
        {
            string cmd = "INSERT INTO Message(studentId,teacherId,type,time,info) VALUES (@studentId,@teacherId,@type,@time,@info) ";
            SqlParameter[] parameters = {
                new SqlParameter("@time", SqlDbType.NVarChar, 50) { Value = time },
                new SqlParameter("@studentId", SqlDbType.Int) { Value = studentId },
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) },
                new SqlParameter("@type", SqlDbType.Int) { Value = type },
                new SqlParameter("@info", SqlDbType.NVarChar, 100) { Value = info }
            };
            int result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, parameters, CommandType.Text);
            return result == 1;
        }

        //学生生成留言recevier是teacher    sender是学生 type=0
        public static bool AddMessage(Student user, int teacherId, int type, string time, string info)
        {
            string cmd = "INSERT INTO Message(studentId,teacherId,type,time,info) VALUES (@studentId,@teacherId,@type,@time,@info) ";
            SqlParameter[] parameters = {
                new SqlParameter("@time", SqlDbType.NVarChar, 50) { Value = time },
                new SqlParameter("@studentId", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) },
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = teacherId },
                new SqlParameter("@type", SqlDbType.Int) { Value = type },
                new SqlParameter("@info", SqlDbType.NVarChar, 100) { Value = info }
            };
            int result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, parameters, CommandType.Text);
            return result == 1;
        }

        //查看学生和教师间是否有留言（教师端查询）
        public static object ReturnTeaMes(Teacher user, int studentId)
        {
            string cmd = "SELECT COUNT(id) FROM Message WHERE studentId=@studentId AND teacherId=@teacherId ";
            SqlParameter[] parameters = {
                new SqlParameter("@studentId", SqlDbType.Int) { Value = studentId },
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) }
            };
            int result = (int)SqlHelper.ExecuteScalar(user.Connection, cmd, parameters, CommandType.Text);
            return result;
        }

        //查看学生和教师间是否有留言（学生端查询）
        public static object ReturnStuMes(Student user, int teacherId)
        {
            string cmd = "SELECT COUNT(id) FROM Message WHERE studentId=@studentId AND teacherId=@teacherId ";
            SqlParameter[] parameters = {
                new SqlParameter("@studentId", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) },
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = teacherId }
            };
            int result = (int)SqlHelper.ExecuteScalar(user.Connection, cmd, parameters, CommandType.Text);
            return result;
        }

        //显示留言（教师端）
        public static DataTable DisplayTeaMes(Teacher user, int studentId)
        {
            string cmd = "SELECT id,studentId,teacherId,info,time FROM Message WHERE studentId=@studentId AND teacherId=@teacherId ORDER BY id DESC";
            SqlParameter[] parameters = {
                new SqlParameter("@studentId", SqlDbType.Int) { Value = studentId },
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) }
            };
            return SqlHelper.ExecuteDataTable(user.Connection, cmd, parameters, CommandType.Text);
        }

        //显示留言（学生端）
        public static DataTable DisplayStuMes(Student user, int teacherId)
        {
            string cmd = "SELECT id,studentId,teacherId,info,time FROM Message WHERE studentId=@studentId AND teacherId=@teacherId ORDER BY id DESC";
            SqlParameter[] parameters = {
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = teacherId },
                new SqlParameter("@studentId", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) }
            };
            return SqlHelper.ExecuteDataTable(user.Connection, cmd, parameters, CommandType.Text);
        }

        //显示发信箱（教师端）
        public static DataTable DisplayTeaSend(Teacher user)
        {
            string cmd = "SELECT id,studentId,info,time FROM Message WHERE teacherId=@teacherId AND type = 1 ORDER BY id DESC";
            SqlParameter[] parameters = {
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) }
            };
            return SqlHelper.ExecuteDataTable(user.Connection, cmd, parameters, CommandType.Text);
        }

        //显示收信箱（教师端）
        public static DataTable DisplayTeaRece(Teacher user)
        {
            string cmd = "SELECT id,studentId,info,time FROM Message WHERE teacherId=@teacherId AND type = 0 ORDER BY id DESC";
            SqlParameter[] parameters = {
                new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) }
            };
            return SqlHelper.ExecuteDataTable(user.Connection, cmd, parameters, CommandType.Text);
        }

        //显示发信箱（学生端）
        public static DataTable DisplayStuSend(Student user)
        {
            string cmd = "SELECT id,teacherId,info,time FROM Message WHERE studentId=@studentId AND type = 0 ORDER BY id DESC";
            SqlParameter[] parameters = {
                new SqlParameter("@studentId", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) }
            };
            return SqlHelper.ExecuteDataTable(user.Connection, cmd, parameters, CommandType.Text);
        }

        //显示收信箱（学生端）
        public static DataTable DisplayStuRece(Student user)
        {
            string cmd = "SELECT id,teacherId,info,time FROM Message WHERE studentId=@studentId AND type = 1 ORDER BY id DESC";
            SqlParameter[] parameters = {
                new SqlParameter("@studentId", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) }
            };
            return SqlHelper.ExecuteDataTable(user.Connection, cmd, parameters, CommandType.Text);
        }
    }
}
