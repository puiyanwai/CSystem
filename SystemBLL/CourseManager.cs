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
    public class CourseManager
    {
        //查看全部发布的课程
        public static DataTable AllCourse()
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT Class.id,Class.name,Teacher.name,Teacher.id,category,time,place,capacity,usualproportion FROM Class,Teacher WHERE Class.teacherId = Teacher.id ORDER BY Class.id ASC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }
        
        //筛选
        public static DataTable SelectCourse(int TeacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT id,name,category,time,place,capacity,usualproportion FROM Class WHERE teacherId=@TeacherId order by id asc";
            SqlParameter[] parameters = {
                    new SqlParameter("@TeacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(TeacherId) }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
            
        }

        //插入、发布课程
        public static bool InsertCourse(Teacher user, string name, string category,
                                        string time, string place, int capacity, float usualproportion)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "INSERT INTO Class(name,teacherId,category,time," +
                "place,capacity,usualproportion) " +
                "VALUES (@name, @teacherId, @category, @time, @place, @capacity, @usualproportion) ";
            SqlParameter[] parameters = {
                 new SqlParameter("@name", SqlDbType.NVarChar, 50) { Value = name },
                 new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) },
                 new SqlParameter("@category", SqlDbType.NVarChar, 50) { Value = category },
                 new SqlParameter("@time", SqlDbType.NVarChar, 50) { Value = time },
                 new SqlParameter("@place", SqlDbType.NVarChar, 50) { Value = place },
                 new SqlParameter("@capacity", SqlDbType.Int, 50) { Value = capacity },
                 new SqlParameter("@usualproportion", SqlDbType.Decimal, 50) { Value = usualproportion }
            };
            int result =  SqlHelper.ExecuteNonQuery(conn, cmd,  parameters, CommandType.Text);
            return result == 1;
        }

        //修改课程信息
        public static bool ModifyCourse(Teacher user,
            int clsid,
            string name,
            string category,
            string time,
            string place,
            int capacity,
            float usualproportion)
        {
            var cmd = "UPDATE Class SET name=@n,category=@c,time=@t,place=@p,capacity=@ca,usualproportion=@u WHERE id=@i";
            var result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, new SqlParameter[] {
                new SqlParameter("@n", SqlDbType.NVarChar, 50) { Value = name },
                new SqlParameter("@c", SqlDbType.NVarChar, 50) { Value = category },
                new SqlParameter("@t", SqlDbType.NVarChar, 50) { Value = time },
                new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = place },
                new SqlParameter("@ca", SqlDbType.Int) { Value = capacity },
                new SqlParameter("@u", SqlDbType.Float) { Value = usualproportion },
                new SqlParameter("@i", SqlDbType.Int) { Value = clsid }
            });
            return result == 1;
        }

        //删除课程前查询是否有人选择了该课程
        public static object CheckDelete(int classId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);
            string cmd = "SELECT COUNT(studentId) FROM ChooseCls WHERE classId = @classId";
            SqlParameter[] parameters = {
                    new SqlParameter("@classId", SqlDbType.Int) { Value = classId }
            };
            int result = (int)SqlHelper.ExecuteScalar(conn, cmd, parameters, CommandType.Text);
            return result;
        }

        //删除课程
        public static bool DeleteCourse(int ClassId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "DELETE FROM Class WHERE id = @ClassId ";

            SqlParameter[] parameters = {
                    new SqlParameter("@ClassId", SqlDbType.Int) { Value = ClassId }
            };

            int result = SqlHelper.ExecuteNonQuery(conn, cmd, parameters, CommandType.Text);
            if (result > 0) return true;
            else return false;
        }

        //判断选课时该课时间是否与已选课程时间相冲突
        public static object CheckTime(int studentId, string time)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);
            string cmd = "SELECT COUNT(classId) FROM ChooseCls,Class WHERE Class.id=ChooseCls.classId AND studentId=@studentId AND time=@time";
            SqlParameter[] parameters = {
                    new SqlParameter("@studentId", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(studentId) },
                    new SqlParameter("@time", SqlDbType.NVarChar, 50) { Value = time }
            };
            int result = (int)SqlHelper.ExecuteScalar(conn, cmd, parameters, CommandType.Text);
            return result;
        }

        //选课
        public static bool ChooseCourse(Student user,int cls, int tea,string clsName)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "INSERT INTO ChooseCls(studentId,classId,teacherId,clsName) VALUES (@stu,@cls,@tea,@clsName)";
            SqlParameter[] parameters = {
                    new SqlParameter("@stu", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) },
                    new SqlParameter("@cls", SqlDbType.Int) { Value = cls },
                    new SqlParameter("@tea", SqlDbType.Int) { Value = tea },
                    new SqlParameter("@clsName", SqlDbType.VarChar, 50) { Value = clsName }
            };
            int result = SqlHelper.ExecuteNonQuery(conn, cmd, parameters, CommandType.Text);
            return result == 1;
        }

        //显示选到的课程
        public static DataTable DisplayCourse(int StudentId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT Class.id,Class.name,Teacher.name,category,time,place,capacity,usualproportion " +
                "FROM Class,Teacher,ChooseCls WHERE ChooseCls.classId = Class.id " +
                "and ChooseCls.teacherId = Teacher.id and ChooseCls.studentId = @StudentId  ORDER BY Class.id ASC";
            SqlParameter[] parameters = {
                    new SqlParameter("@StudentId", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(StudentId) }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //退课
        public static bool DropCourse(int Clsid)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "DELETE FROM ChooseCls WHERE classId = @Clsid AND usualgra IS NULL ";

            SqlParameter[] parameters = {
                    new SqlParameter("@Clsid", SqlDbType.Int) { Value = Clsid }
            };

            int result = SqlHelper.ExecuteNonQuery(conn, cmd, parameters, CommandType.Text);
            if (result > 0) return true;
            else return false;
        }

        //更新课程数量
        public static bool UpdateCourse(int num,int cid)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "UPDATE Class SET capacity = @num WHERE id = @cid";
            SqlParameter[] parameters = {
                    new SqlParameter("@num", SqlDbType.Int) { Value = num },
                    new SqlParameter("@cid", SqlDbType.Int) { Value = cid }
            };
            int result = SqlHelper.ExecuteNonQuery(conn, cmd, parameters, CommandType.Text);
            return result == 1;

        }

        //查询、筛选课程
        public static DataTable SearchClass(string option)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT Class.id,Class.name,Teacher.name,Teacher.id,category,time,place,capacity,usualproportion FROM Class,Teacher WHERE Class.teacherId = Teacher.id " + option + " ORDER BY Class.id ASC";
            return SqlHelper.ExecuteDataTable(conn, cmd, null, CommandType.Text);
        }

        public static IEnumerable<Class> GetAssociatedClasses(Teacher user)
        {
            var cmd = "SELECT id,name,category,time,place,capacity,usualproportion FROM Class WHERE teacherId=@i";
            var result = SqlHelper.ExecuteDataTable(user.Connection, cmd, new SqlParameter[]
            {
                new SqlParameter("@i", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) }
            });
            if (result == null)
                return Enumerable.Empty<Class>();
            return result.Rows.Cast<DataRow>().Select(row => new Class()
            {
                Id = (int)row.ItemArray[0],
                Name = row.ItemArray[1] as string,
                TeacherId = user.Id,
                Category = row.ItemArray[2] as string,
                Time = row.ItemArray[3] as string,
                Place = row.ItemArray[4] as string,
                Capacity = (int)row.ItemArray[5],
                UsualProportion = Convert.ToSingle(row.ItemArray[6])
            });

            
        }
    }
}
