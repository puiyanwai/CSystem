using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SystemDAL;
using Common;
using Model;

namespace SystemBLL
{
    public class SignManager
    {
        //显示选课后的签到课程名单
        public static DataTable DisplaySignList(int stuid)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT Class.id,Class.name,Teacher.id,Teacher.name,category,time,place " +
                "FROM Class,Teacher,ChooseCls WHERE ChooseCls.classId = Class.id " + 
                "and ChooseCls.teacherId = Teacher.id and ChooseCls.studentId = @stuid  ORDER BY Class.id ASC";
            SqlParameter[] parameters = {
                    new SqlParameter("@stuid", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(stuid) }
                };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //签到
        public static bool SignUp(Student user,string stuname,int clsid,string ip,string time,string status)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "INSERT INTO Sign(studentId,stuName,classId,ip,time,status) VALUES (@stu,@stuname,@cls,@ip,@time,@status)";
            SqlParameter[] parameters = {
                    new SqlParameter("@stu", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) },
                    new SqlParameter("@stuname", SqlDbType.VarChar, 50) { Value = stuname },
                    new SqlParameter("@cls", SqlDbType.Int) { Value = clsid },
                    new SqlParameter("@ip", SqlDbType.VarChar, 50) { Value = ip },
                    new SqlParameter("@time", SqlDbType.VarChar, 50) { Value = time },
                    new SqlParameter("@status", SqlDbType.VarChar, 50) { Value = status }
            };
            int result = SqlHelper.ExecuteNonQuery(conn, cmd, parameters, CommandType.Text);
            return result == 1;
        }

        //学生查看自己签到情况
        public static DataTable IPaddress(int stuid)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,classId,ip,time,status FROM Sign WHERE studentId = @stuid ";
            SqlParameter[] parameters = {
                    new SqlParameter("@stuid", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(stuid) }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //判断某学生是否签到某节课
        public static object CheckSignUp(int stuid,int clsid)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT COUNT(id) FROM Sign WHERE studentId = @stuid AND classId = @clsid ";
            SqlParameter[] parameters = {
                    new SqlParameter("@stuid", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(stuid) },
                    new SqlParameter("@clsid", SqlDbType.Int) { Value = clsid }
                };
            int result = (int)SqlHelper.ExecuteScalar(conn, cmd, parameters, CommandType.Text);
            return result;
        }

        //显示某教师负责全部课程学生的全部签到情况
        public static DataTable DisplaySignUp(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT Sign.id,Sign.studentId,stuName,Sign.classId,ip,time,status FROM Sign,ChooseCls " +
                         "WHERE ChooseCls.teacherId = @teacherId AND ChooseCls.studentId = Sign.studentId " +
                         "AND ChooseCls.classId = Sign.classId ORDER BY Sign.id ASC ";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //显示某教师负责的课的学生签到情况
        public static DataTable DisplayDifferentSignUp(int teacherId,string option)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT Sign.id,Sign.studentId,stuName,ip,time,status FROM Sign,ChooseCls " +
                         "WHERE ChooseCls.teacherId = @teacherId AND ChooseCls.studentId = Sign.studentId " + option + " ORDER BY Sign.id ASC ";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
                };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //删除学生的签到记录
        public static bool DeleteSignUpRecord(int id)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "DELETE FROM Sign WHERE id = @id ";

            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int) { Value = id }
                };

            int result = SqlHelper.ExecuteNonQuery(conn, cmd, parameters, CommandType.Text);
            if (result > 0) return true;
            else return false;
        }
    }
}
