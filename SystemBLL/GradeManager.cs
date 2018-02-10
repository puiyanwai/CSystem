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
    public class GradeManager
    {
        //Display all students
        public static DataTable AllStudent()
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,name,sex,college FROM Student ORDER BY id ASC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        //Select and Diaplay the optional students
        public static DataTable SelectStudent(string option)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,name,sex,college FROM Student " + option + " ORDER BY id ASC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        //筛选出一个教师负责的全部课程学生名单
        public static DataTable DisplayAllCourse(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT Student.id,name,college,classId,usualgra,finalgra,totalgra FROM Student,ChooseCls " +
                         "WHERE teacherId = @teacherId AND Student.id = ChooseCls.studentId ORDER BY Student.id ASC";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
                };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //combobox选定数据库中数据
        public static DataTable DisplayCombobox(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id FROM Class WHERE teacherId = @teacherId";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
                };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //筛选出一个教师负责多少门课程
        public static object ReturnNum(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT COUNT(id) FROM Class WHERE teacherId = @teacherId";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
                };
            int result =(int)SqlHelper.ExecuteScalar(conn, cmd, parameters, CommandType.Text);
            return result;
        }
        

        //根据combobox不同选项显示不同课程的学生名单
        public static DataTable SelectDifferentGrade(int teacherId,string option)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT Student.id,name,college,classId,usualgra,finalgra,totalgra FROM Student,ChooseCls " +
                         "WHERE teacherId = @teacherId AND Student.id = ChooseCls.studentId " + option + " ORDER BY Student.id ASC";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
                };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //添加、修改学生成绩
        public static bool UpdateClass(int stuid, int clsid, double usualgra, double finalgra, double totalgra)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            var cmd = "UPDATE ChooseCls SET usualgra=@us,finalgra=@fi,totalgra=@to WHERE studentId=@stuid AND classId=@clsid";
            var result = SqlHelper.ExecuteNonQuery(conn, cmd, new SqlParameter[] {
                new SqlParameter("@us", SqlDbType.Float) { Value = usualgra },
                new SqlParameter("@fi", SqlDbType.Float) { Value = finalgra },
                new SqlParameter("@to", SqlDbType.Float) { Value = totalgra },
                new SqlParameter("@stuid", SqlDbType.Int) { Value = stuid },
                new SqlParameter("@clsid", SqlDbType.Int) { Value = clsid }
            });
            return result == 1;
        }

        //显示添加、修改成绩后的表格
        public static DataTable DisplayGrade(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT Student.id,name,college,ChooseCls.classId,usualgra,finalgra,totalgra " +
                         "FROM Student,ChooseCls WHERE ChooseCls.teacherId = @teacherId AND Student.id = ChooseCls.studentId ";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
                };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //筛选出所选课程的平时成绩权重
        public static DataTable ReturnProportion(int classId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT usualproportion FROM Class WHERE id = @classId";
            SqlParameter[] parameters = {
                    new SqlParameter("@classId", SqlDbType.Int) { Value = classId }
                };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //学生端显示有成绩的出来
        public static DataTable DisplayStudentGrade(int stuid)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT classId,clsName,teacherId,usualgra,finalgra,totalgra FROM ChooseCls WHERE studentId = @stuid AND usualgra IS NOT NULL ";
            SqlParameter[] parameters = {
                    new SqlParameter("@stuid", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(stuid) }
                };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }


    }
}
