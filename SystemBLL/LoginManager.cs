using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SystemDAL;
using Common;
using Model;

namespace SystemBLL
{
    public class LoginManager
    {
        //Login
        public static Student StudentLogin(int id, string password)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            var cmd = "SELECT name,sex,college,phone FROM Student WHERE id=@u and password=@p";
            var result = SqlHelper.ExecuteDataTable(conn, cmd, new SqlParameter[] {
                    new SqlParameter("@u", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(id) },
                    new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = Utilities.EncryptPassword(password) }
                });

            if (result.Rows.Count == 1 &&
                result.Rows[0].ItemArray.Length == 4)
            {
                var dat = result.Rows[0].ItemArray;
                return new Student(conn, id,
                    dat[0] as string,
                    (SexType)(int)dat[1],
                    dat[2] as string,
                    dat[3] as string);
            }
            else
            {
                conn.Close();
                return null;
            }
        }

        public static Teacher TeacherLogin(int id, string password)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "SELECT name,sex,brand,phone FROM Teacher WHERE id=@u and password=@p ";
            var result = SqlHelper.ExecuteDataTable(
                conn, cmd,
                parameters: new SqlParameter[] {
                    new SqlParameter("@u", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(id) },
                    new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = Utilities.EncryptPassword(password) }
                });
            if (result.Rows.Count == 1 &&
                result.Rows[0].ItemArray.Length == 4)
            {
                var dat = result.Rows[0].ItemArray;
                return new Teacher(conn, id,
                    dat[0] as string,
                    (SexType)(int)dat[1],
                    dat[2] as string,
                    dat[3] as string);
            }
            else
            {
                conn.Close();
                return null;
            }
        }

        public static Admin AdminLogin(int id, string password)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);
#if !DEBUG
#error Make sure Data Table is correct
#endif
            string cmd = "SELECT id FROM Admin WHERE id=@u and password=@p ";
            var result = SqlHelper.ExecuteDataTable(
                conn, cmd,
                parameters: new SqlParameter[] {
                    new SqlParameter("@u", SqlDbType.Int) { Value = id },
                    new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = Utilities.EncryptPassword(password) }
                });
            if (result.Rows.Count == 1)
            {
                return new Admin(conn, id);
            }
            else
            {
                conn.Close();
                return null;
            }
        }

        public static Tuple<int, string>[] AdminFetchTeachers(Admin admin)
        {
            var cmd = "SELECT id,name FROM Teacher";
            var res = SqlHelper.ExecuteDataTable(admin.Connection, cmd);
            return res.Rows.Cast<DataRow>().Select(r => Tuple.Create(
               Convert.ToInt32(r.ItemArray[0]),
               Convert.ToString(r.ItemArray[1]))).ToArray();
        }

        //Register
        public static int StuRegister(string name, SexType sex, string college, string phone, string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (college == null)
                throw new ArgumentNullException(nameof(college));

            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "INSERT INTO Student(name,sex,college,phone,password) OUTPUT Inserted.id VALUES (@n,@s,@c,@p,@pw)";
            var result = SqlHelper.ExecuteScalar(conn, cmd, parameters: new SqlParameter[] {
                new SqlParameter("@n", SqlDbType.NVarChar, 50) { Value = name },
                new SqlParameter("@s", SqlDbType.Int) { Value = (int)sex },
                new SqlParameter("@c", SqlDbType.NVarChar, 50) { Value = college },
                new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = phone },
                new SqlParameter("@pw", SqlDbType.NVarChar, 50) { Value = Utilities.EncryptPassword(password) }
            });

            return result != null ? (int)result : -1;
        }

        public static int TeaRegister(string name, SexType sex, string brand, string phone, string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (brand == null)
                throw new ArgumentNullException(nameof(brand));

            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);

            string cmd = "INSERT INTO Teacher (name,sex,brand,phone,password) OUTPUT Inserted.id VALUES (@n,@s,@b,@p,@pw)";
            var result = SqlHelper.ExecuteScalar(conn, cmd, parameters: new SqlParameter[] {
                new SqlParameter("@n", SqlDbType.NVarChar, 50) { Value = name },
                new SqlParameter("@s", SqlDbType.Int) { Value = (int)sex },
                new SqlParameter("@b", SqlDbType.NVarChar, 50) { Value = brand },
                new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = phone },
                new SqlParameter("@pw", SqlDbType.NVarChar, 50) { Value = Utilities.EncryptPassword(password) }
            });

            return result != null ? (int)result : -1;
        }

        //显示主页教师个人信息
        public static DataTable DisplayTeaInfo(int teacherId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);
            string cmd = "SELECT name,id,sex,brand,phone FROM Teacher WHERE id = @teacherId";
            SqlParameter[] parameters = {
                    new SqlParameter("@teacherId", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(teacherId) }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //显示主页学生个人信息
        public static DataTable DisplayStuInfo(int studentId)
        {
            var conn = SqlHelper.OpenDatabase(
                BLLConfig.AdminUserName,
                BLLConfig.AdminPassword,
                BLLConfig.DefaultSource,
                BLLConfig.DbName);
            string cmd = "SELECT name,id,sex,college,phone FROM Student WHERE id = @studentId";
            SqlParameter[] parameters = {
                    new SqlParameter("@studentId", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(studentId) }
            };
            return SqlHelper.ExecuteDataTable(conn, cmd, parameters, CommandType.Text);
        }

        //更新教师信息
        public static bool UpdateTeaInfo(Teacher user, string name, SexType sex, string brand, string phone)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (brand == null)
                throw new ArgumentNullException(nameof(brand));
            if (phone == null)
                throw new ArgumentNullException(nameof(phone));

            var cmd = "UPDATE Teacher SET name=@n,sex=@s,brand=@b,phone=@p WHERE id=@id ";
            var result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, new SqlParameter[] {
                new SqlParameter("@n", SqlDbType.VarChar, 50) { Value = name },
                new SqlParameter("@s", SqlDbType.Int) { Value = (int)sex },
                new SqlParameter("@b", SqlDbType.NVarChar, 50) { Value = brand },
                new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = phone },
                new SqlParameter("@id", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) } 
            });
            return result == 1;
        }

        //更新学生信息
        public static bool UpdateStuInfo(Student user, string name, SexType sex, string college, string phone)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (college == null)
                throw new ArgumentNullException(nameof(college));
            if (phone == null)
                throw new ArgumentNullException(nameof(phone));

            var cmd = "UPDATE Student SET name=@n,sex=@s,college=@c,phone=@p WHERE id=@id ";
            var result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, new SqlParameter[] {
                new SqlParameter("@n", SqlDbType.VarChar, 50) { Value = name },
                new SqlParameter("@s", SqlDbType.Int) { Value = (int)sex },
                new SqlParameter("@c", SqlDbType.NVarChar, 50) { Value = college },
                new SqlParameter("@p", SqlDbType.NVarChar, 50) { Value = phone },
                new SqlParameter("@id", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) }  
            });
            return result == 1;
        }

        //修改教师的密码
        public static bool UpdateTeaPwd(Teacher user, string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            var cmd = "UPDATE Teacher SET password=@p WHERE id=@id ";
            var result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, new SqlParameter[] {
                new SqlParameter("@p", SqlDbType.VarChar, 50) { Value = Utilities.EncryptPassword(password) },
                new SqlParameter("@id", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) } 
            });
            return result == 1;
        }

        //修改学生的密码
        public static bool UpdateStuPwd(Student user, string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            var cmd = "UPDATE Student SET password=@p WHERE id=@id ";
            var result = SqlHelper.ExecuteNonQuery(user.Connection, cmd, new SqlParameter[] {
                new SqlParameter("@p", SqlDbType.VarChar, 50) { Value = Utilities.EncryptPassword(password) },
                new SqlParameter("@id", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) }
            });
            return result == 1;
        }

        //检查教师密码是否与数据库中的相同
        public static object CheckTeaPwd(Teacher user, string password)
        {
            string cmd = "SELECT COUNT(id) FROM Teacher WHERE password=@p AND id=@id ";
            SqlParameter[] parameters = {
                    new SqlParameter("@p", SqlDbType.VarChar, 50) { Value = Utilities.EncryptPassword(password) },
                    new SqlParameter("@id", SqlDbType.Int) { Value = Utilities.TeaIdConvertToDbId(user.Id) }
                };
            int result = (int)SqlHelper.ExecuteScalar(user.Connection, cmd, parameters, CommandType.Text);
            return result;
        }

        //检查学生密码是否与数据库中的相同
        public static object CheckStuPwd(Student user, string password)
        {
            string cmd = "SELECT COUNT(id) FROM Student WHERE password=@p AND id=@id ";
            SqlParameter[] parameters = {
                    new SqlParameter("@p", SqlDbType.VarChar, 50){ Value = Utilities.EncryptPassword(password) },
                    new SqlParameter("@id", SqlDbType.Int) { Value = Utilities.StuIdConvertToDbId(user.Id) }
                };
            int result = (int)SqlHelper.ExecuteScalar(user.Connection, cmd, parameters, CommandType.Text);
            return result;
        }

    }
}
