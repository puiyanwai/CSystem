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
    public class AdminManager
    {
        //显示除了自身之外全部教师信息
        public static DataTable DisplayTea(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,name,sex,brand FROM Teacher WHERE id !=@teacherId ORDER BY id ASC";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        //筛选出教师的数量
        public static object ReturnTeaNum(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT COUNT(id) FROM Teacher WHERE id != @teacherId ";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
            };
            int result = (int)SqlHelper.ExecuteScalar(conn, cmd, parameters, CommandType.Text);
            return result;
        }

        //combobox选定数据库中数据
        public static DataTable DisplayCombobox(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id FROM Teacher WHERE id != @teacherId";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //按照combobox里的教师id显示教师负责的课程
        public static DataTable SelectTeaCls(string option)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,name,teacherId,category,time,place,capacity,usualproportion FROM Class " + option + " ORDER BY id ASC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        //显示除自己以外全部教师的课程
        public static DataTable DisplayTeaCls(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,name,teacherId,category,time,place,capacity,usualproportion FROM Class WHERE id !=@teacherId ORDER BY id ASC";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        //显示教师负责的课程的学生的成绩
        public static DataTable DisplayAllStuGra(string option)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT classId,Student.id,name,college,usualgra,finalgra,totalgra FROM Student,ChooseCls " +
                         "WHERE Student.id = ChooseCls.studentId " + option + " ORDER BY classId ASC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }
    }
}
